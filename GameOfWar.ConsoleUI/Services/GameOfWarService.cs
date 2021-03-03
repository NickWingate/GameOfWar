using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GameOfWar.ConsoleUI.Services
{
	public class GameOfWarService : IGameOfWarService
	{
		private readonly ILogger<GameOfWarService> _logger;
		private readonly IConfiguration _config;

		public GameOfWarService(ILogger<GameOfWarService> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
		}
		public void Run()
		{
			_logger.LogInformation("App started");
			var playerCount = _config.GetValue<int>("PlayerCount");
		}
	}
}