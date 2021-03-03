using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Domain.Services
{
	public interface IDealCardsService
	{
		void Deal(List<Player> players, Deck deck);
	}
}