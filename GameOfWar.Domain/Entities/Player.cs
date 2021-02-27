using System.Collections.Generic;

namespace GameOfWar.Domain.Entities
{
	public class Player
	{
		public Player(string name)
		{
			Name = name;
		}
		public string Name { get; set; }
		public List<Card> Hand { get; set; }
		public Card CurrentCard { get; set; }
		
		public override string ToString()
		{
			return Name;
		}
	}
}