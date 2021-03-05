using System;
using GameOfWar.Application;
using GameOfWar.Application.Factories;
using GameOfWar.Application.Services;
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
		private readonly IPlayerFactory _playerFactory;
		private readonly IWarService _warService;
		private readonly IDrawService _drawService;

		public GameOfWarService(
			ILogger<GameOfWarService> logger,
			IConfiguration config,
			IDealCardsService dealCardsService,
			IWinnerService winnerService, 
			IPlayerFactory playerFactory,
			IWarService warService, IDrawService drawService)
		{
			_logger = logger;
			_config = config;
			_dealCardsService = dealCardsService;
			_winnerService = winnerService;
			_playerFactory = playerFactory;
			_warService = warService;
			_drawService = drawService;
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
				_playerFactory,
				_warService,
				_drawService,
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