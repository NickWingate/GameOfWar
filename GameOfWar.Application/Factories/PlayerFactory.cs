using System;
using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Application.Factories
{
	public class PlayerFactory : IPlayerFactory
	{
		private readonly Action<string> _outputProvider;
		private readonly Func<string> _inputProvider;

		public PlayerFactory(
			Func<string> inputProvider, 
			Action<string> outputProvider)
		{
			_inputProvider = inputProvider;
			_outputProvider = outputProvider;
		}

		public PlayerFactory() : 
			this(Console.ReadLine, Console.WriteLine)
		{
			
		}

		public List<Player> CreatePlayers(int playerCount)
		{
			var players = new List<Player>(playerCount);
			for (int i = 0; i < playerCount; i++)
			{
				_outputProvider($"Name for player {i+1}: ");
				var name = _inputProvider();
				players.Add(new Player(name));
			}

			return players;
		}
	}
}