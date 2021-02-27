using GameOfWar.Domain.Enums;

namespace GameOfWar.Domain.Entities
{
	public class Card
	{
		public Card(Suit suit, Rank rank)
		{
			Suit = suit;
			Rank = rank;
		}

		public Suit Suit { get; set; }
		public Rank Rank { get; set; }

		public override string ToString()
		{
			return $"{Rank} of {Suit}";
		}
	}
}