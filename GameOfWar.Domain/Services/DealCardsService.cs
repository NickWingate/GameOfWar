using System;
using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Domain.Services
{
	public class DealCardsService : IDealCardsService
	{
		public void Deal(List<Player> players, Deck deck)
		{
			var cardOverflow = deck.Count % players.Count;
			if (cardOverflow != 0)
			{
				var unusedCards = new List<Card>();
				for (int i = 0; i < cardOverflow; i++)
				{
					unusedCards.Add(deck.DrawCard());
				}
				
			}
			while (!deck.IsEmpty)
			{
				foreach (var player in players)
				{
					player.Hand.Add(deck.DrawCard());
				}
			}
		}
	}
}