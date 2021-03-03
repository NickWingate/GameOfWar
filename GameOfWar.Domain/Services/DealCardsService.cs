using System;
using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Domain.Services
{
	public class DealCardsService : IDealCardsService
	{
		public void Deal(List<Player> players, Deck deck)
		{
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