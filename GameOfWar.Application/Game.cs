using System;
using System.Collections.Generic;
using GameOfWar.Application.Factories;
using GameOfWar.Application.Services;
using GameOfWar.Domain.Entities;
using GameOfWar.Domain.Services;

namespace GameOfWar.Application
{
	public class Game
	{
		private readonly IDealCardsService _dealCardsService;
		private readonly IWinnerService _winnerService;
		private readonly Func<string> _inputProvider;
		private readonly Action<string> _outputProvider;
		private readonly IWarService _warService;
		private readonly IDrawService _drawService;

		public Game(
			IDealCardsService dealCardsService, 
			IWinnerService winnerService,
			IPlayerFactory playerFactory,
			IWarService warService,
			IDrawService drawService,
			Func<string> inputProvider,
			Action<string> outputProvider,
			int playerCount)
		{
			_dealCardsService = dealCardsService;
			_winnerService = winnerService;
			_inputProvider = inputProvider;
			_outputProvider = outputProvider;
			_drawService = drawService;
			_warService = warService;
			
			Players = playerFactory.CreatePlayers(playerCount);
		}

		public List<Player> Players { get; set; }
		public Deck Deck { get; set; }
		public int MinimumHandSize { get; set; } = 0;
		public int CardsDealtInWar { get; set; } = 3;

		public void Deal()
		{
			Deck = new Deck();
			Deck.Shuffle();
			_dealCardsService.Deal(Players, Deck);
		}

		public void Play()
		{
			while (!ReachedMinimumHandCount(Players))
			{
				var roundCards = new List<Card>();
				var roundWinner = FindRoundWinner(roundCards, Players);
				while (roundWinner == null)
				{
					var drawnPlayers = _drawService.FindDrawnPlayers(Players);
					_outputProvider($"There was a draw with these players: {string.Join(", ", drawnPlayers)}");
					_warService.GoToWar(roundCards, drawnPlayers, CardsDealtInWar);
					roundWinner = FindRoundWinner(roundCards, drawnPlayers);
				}

				RoundWonProcedure(roundWinner, roundCards);
			}

			var finalWinner = _winnerService.DetermineFinalWinner(Players);
			_outputProvider($"{finalWinner} won with a total of {finalWinner.Score} wins" +
			                $" and a hand of {finalWinner.Hand.Count} cards");
		}

		private Player FindRoundWinner(ICollection<Card> roundCards, List<Player> players)
		{
			AddRoundCards(roundCards, players);
			var roundWinner = _winnerService.DetermineWinner(players);
			OutputCardsDrawn(players);
			return roundWinner;
		}

		private void RoundWonProcedure(Player roundWinner, List<Card> roundCards)
		{
			roundWinner.Score++;
			roundWinner.Hand.AddRange(roundCards);
			_outputProvider($"{roundWinner} won this round, they gained {roundCards.Count} cards and have " +
			                $"{roundWinner.Score} wins and {roundWinner.Hand.Count} cards in their hand");
			_inputProvider();
		}

		private void OutputCardsDrawn(IEnumerable<Player> players)
		{
			foreach (var player in players)
			{
				_outputProvider($"{player} drew {player.CurrentCard}");
			}
		}

		private static void AddRoundCards(ICollection<Card> roundCards, IEnumerable<Player> players)
		{
			foreach (var player in players)
			{
				roundCards.Add(player.DrawCard());
			}
		}

		private bool ReachedMinimumHandCount(IEnumerable<Player> players)
		{
			foreach (var player in players)
			{
				if (player.Hand.Count < MinimumHandSize)
				{
					return true;
				}
			}

			return false;
		}
	}
}