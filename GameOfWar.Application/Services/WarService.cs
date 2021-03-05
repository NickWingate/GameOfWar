using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Application.Services
{
	public class WarService : IWarService
	{
		public void GoToWar(ICollection<Card> roundCards, IEnumerable<Player> players, int cardsToAdd)
		{
			foreach (var player in players)
			{
				for (int i = 0; i < cardsToAdd; i++)
				{
					roundCards.Add(player.DrawCard());
				}
			}
		}
	}
}