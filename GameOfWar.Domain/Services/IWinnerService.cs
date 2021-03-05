using System.Collections;
using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Domain.Services
{
	public interface IWinnerService
	{
		public Player DetermineWinner(Player player1, Player player2);
		public Player DetermineWinner(List<Player> players);
	}
}