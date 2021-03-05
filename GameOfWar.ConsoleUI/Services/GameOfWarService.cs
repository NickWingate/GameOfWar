using System;
using GameOfWar.Application;
using GameOfWar.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GameOfWar.ConsoleUI.Services
{
	public class GameOfWarService : IGameOfWarService
	{
		private readonly ILogger<GameOfWarService> _logger;
		private readonly IConfiguration _config;
		private readonly IDealCardsService _dealCardsService;
		private readonly IWinnerService _winnerService;


		public GameOfWarService(
			ILogger<GameOfWarService> logger,
			IConfiguration config,
			IDealCardsService dealCardsService,
			IWinnerService winnerService)
		{
			_logger = logger;
			_config = config;
			_dealCardsService = dealCardsService;
			_winnerService = winnerService;

		}
		public void Run()
		{
			_logger.LogInformation("App started");
			var playerCount = _config.GetValue<int>("PlayerCount");
			var minimumHandSize = _config.GetValue<int>("MinimumHandSize");
			var cardsDealtInWar = _config.GetValue<int>("CardsDealtInWar");
			
			var game = new Game(
				_dealCardsService,
				_winnerService,
				Console.ReadLine,
				Console.WriteLine,
				playerCount)
			{
				MinimumHandSize = minimumHandSize,
				CardsDealtInWar = cardsDealtInWar,
				
			};
			game.Deal();
			game.Play();
		}
	}
}