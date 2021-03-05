using System.Collections.Generic;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Application.Services
{
	public interface IWarService
	{
		void GoToWar(ICollection<Card> roundCards, IEnumerable<Player> players, int cardsToAdd);
	}
}