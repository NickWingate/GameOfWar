using System;
using System.IO;
using GameOfWar;
using GameOfWar.Application;
using GameOfWar.Application.Factories;
using GameOfWar.Application.Services;
using GameOfWar.ConsoleUI.Services;
using GameOfWar.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace GameOfWar.ConsoleUI
{
	static class Program
	{
		static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder();
			BuildConfig(builder);

			var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(builder.Build())
				.Enrich.FromLogContext()
				.WriteTo.File($"{projectDirectory}/logs/log-.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();

			var host = Host.CreateDefaultBuilder()
				.ConfigureServices((context, services) =>
				{
					services.AddTransient<IGameOfWarService, GameOfWarService>();
					services.AddTransient<IDealCardsService, DealCardsService>();
					services.AddTransient<IWinnerService, WinnerService>();
					services.AddTransient<IPlayerFactory, PlayerFactory>();
					services.AddTransient<IWarService, WarService>();
					services.AddTransient<IDrawService, DrawService>();
				})
				.UseSerilog()
				.Build();

			var service = ActivatorUtilities.CreateInstance<GameOfWarService>(host.Services);
			service.Run();
		}

		private static void Play(Game gameOfWar)
		{
			
		}

		private static void BuildConfig(IConfigurationBuilder builder)
		{
			builder.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile(
					$"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
					optional: true)
				.AddEnvironmentVariables();
		}
	}
}