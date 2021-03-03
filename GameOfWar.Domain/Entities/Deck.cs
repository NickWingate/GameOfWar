using System;
using System.Collections.Generic;
using GameOfWar.Domain.Enums;

namespace GameOfWar.Domain.Entities
{
	public class Deck
	{
		public List<Card> Cards { get; set; } = new List<Card>(52);
		public int Count => Cards.Count;

		public bool IsEmpty => Count == 0;
		
		public Deck()
		{
			GenerateCards();
		}

		public void ShuffleDeck()
		{
			var randomNumberGenerator = new Random();
			for (int n = Cards.Count - 1; n > 0; n--)
			{
				var randomNumber = randomNumberGenerator.Next(n + 1);
				var tempCard = Cards[n];
				Cards[n] = Cards[randomNumber];
				Cards[randomNumber] = tempCard;
			}
		}
		private void GenerateCards()
		{
			foreach (var suit in (Suit[]) Enum.GetValues(typeof(Suit)))
			{
				foreach (var rank in (Rank[]) Enum.GetValues(typeof(Rank)))
				{
					Cards.Add(new Card(suit, rank));
				}
			}
		}

		public Card DrawCard()
		{
			if (Cards.Count > 0)
			{
				var tempCard = Cards[0];
				Cards.RemoveAt(0);
				return tempCard;
			}
			else
			{
				throw new IndexOutOfRangeException("Deck is empty");
			}
		}
		
	}
}