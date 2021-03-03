using System.ComponentModel.DataAnnotations;
using GameOfWar.Domain.Entities;

namespace GameOfWar.Application.Services
{
	public interface IDealCardsService
	{
		void DealCards(Player player1, Player player2, Deck deck);
	}
}