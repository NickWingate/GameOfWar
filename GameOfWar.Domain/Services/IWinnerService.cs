using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Domain.Services
{
	public interface IWinnerService
	{
		Player DetermineWinner(Player player1, Player player2);
		Player DetermineWinner(List<Player> players);
		Player DetermineFinalWinner(List<Player> players);
	}
}