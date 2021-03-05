using System.Collections.Generic;

namespace GameOfWar.Domain.Entities
{
	public class Player
	{
		public Player(string name)
		{
			Name = name;
			Score = 0;
		}
		public string Name { get; set; }
		public List<Card> Hand { get; set; } = new List<Card>();
		public Card CurrentCard { get; set; }
		public int Score { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public Card DrawCard()
		{
			CurrentCard = Hand[0];
			Hand.RemoveAt(0);
			return CurrentCard;
		}
	}
}