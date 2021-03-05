using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Domain.Services
{
	public class WinnerService : IWinnerService
	{
		public Player DetermineWinner(Player player1, Player player2)
		{
			if (player1.CurrentCard.Rank == player2.CurrentCard.Rank)
			{
				return null;
			}

			return CompareRanks(player1, player2);
		}

		public Player DetermineWinner(List<Player> players)
		{
			var currentWinner = players[0];
			foreach (var player in players)
			{
				currentWinner = DetermineWinner(currentWinner, player) ?? currentWinner;
			}

			foreach (var player in players)
			{
				if (player.CurrentCard.Rank == currentWinner.CurrentCard.Rank
				&& player != currentWinner)
				{
					return null;
				}
			}

			return currentWinner;
		}

		private static Player CompareRanks(Player player1, Player player2)
		{
			var player1RankValue = (int) player1.CurrentCard.Rank;
			var player2RankValue = (int) player2.CurrentCard.Rank;
			return player1RankValue > player2RankValue ? player1 : player2;
		}
	}
}