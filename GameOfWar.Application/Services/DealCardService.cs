using GameOfWar.Domain.Entities;

namespace GameOfWar.Application.Services
{
	public class DealCardService : IDealCardsService
	{
		public void DealCards(Player player1, Player player2, Deck deck)
		{
			while (!deck.IsEmpty)
			{
				player1.Hand.Add(deck.DrawCard());
				player2.Hand.Add(deck.DrawCard());
			}
		}
	}
}