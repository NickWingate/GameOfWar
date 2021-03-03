using System.Collections.Generic;
using GameOfWar.Domain.Entities;
using GameOfWar.Domain.Services;

namespace GameOfWar.Application
{
	public class Game
	{
		private readonly IDealCardsService _dealCardsService;

		public Game(IDealCardsService dealCardsService)
		{
			_dealCardsService = dealCardsService;
		}

		public List<Player> Players { get; set; } = new List<Player>();
		public Deck Deck { get; set; }

		public void Deal()
		{
			_dealCardsService.Deal(Players, Deck);
		}
	}
}