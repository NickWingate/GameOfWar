using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Application.Services
{
	public interface IDrawService
	{
		List<Player> FindDrawnPlayers(List<Player> players);
	}
}