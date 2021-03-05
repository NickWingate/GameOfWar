using System;
using System.Collections.Generic;
using GameOfWar.Domain.Entities;
using GameOfWar.Domain.Enums;

namespace GameOfWar.Application.Services
{
	public class DrawService : IDrawService
	{
		public List<Player> FindDrawnPlayers(List<Player> players)
		{
			var rankFrequencyTable = GenerateRankFrequencyTable(players);
			if (!HighestRankDrawn(rankFrequencyTable))
			{
				throw new ArgumentException("The highest rank was not drawn, a definite winner exists");
			}
			var highestDrawnRank = FindHighestDrawnRank(rankFrequencyTable);
			var drawnPlayers = FindPlayersWithCardRank(players, highestDrawnRank);
			return drawnPlayers;
		}

		private bool HighestRankDrawn(Dictionary<Rank, int> rankFrequencyTable)
		{
			var highestRank = Rank.Ace;
			foreach (var kvp in rankFrequencyTable)
			{
				if ((int) kvp.Key > (int) highestRank && kvp.Value > 0)
				{
					highestRank = kvp.Key;
				}
			}

			if (rankFrequencyTable[highestRank] == 1)
			{
				return false;
			}

			return true;
		}

		private List<Player> FindPlayersWithCardRank(List<Player> players, Rank rankToFind)
		{
			var sameRankedPlayers = new List<Player>();
			foreach (var player in players)
			{
				if (player.CurrentCard.Rank == rankToFind)
				{
					sameRankedPlayers.Add(player);
				}
			}

			return sameRankedPlayers;
		}

		private Rank FindHighestDrawnRank(Dictionary<Rank, int> rankFrequencyTable)
		{
			var highestRank = Rank.Ace;
			foreach (var kvp in rankFrequencyTable)
			{
				if (kvp.Value >= 2)
				{
					highestRank = kvp.Key;
				}
			}

			return highestRank;
		}

		private Dictionary<Rank, int> GenerateRankFrequencyTable(List<Player> players)
		{
			var rankFrequencyTable = new Dictionary<Rank, int>();
			foreach (var rank in (Rank[]) Enum.GetValues(typeof(Rank)))
			{
				rankFrequencyTable.Add(rank, 0);
			}

			foreach (var player in players)
			{
				rankFrequencyTable[player.CurrentCard.Rank]++;
			}

			return rankFrequencyTable;
		}
	}
}