using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Application.Factories
{
	public interface IPlayerFactory
	{
		List<Player> CreatePlayers(int playerCount);
	}
}