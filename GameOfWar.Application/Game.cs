using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameOfWar.Application.Factories;
using GameOfWar.Application.Services;
using GameOfWar.Domain.Entities;
using GameOfWar.Domain.Enums;
using GameOfWar.Domain.Services;

namespace GameOfWar.Application
{
	public class Game
	{
		private readonly IDealCardsService _dealCardsService;
		private readonly IWinnerService _winnerService;
		private readonly IPlayerFactory _playerFactory;
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
			_playerFactory = playerFactory;
			_warService = warService;
			Players = CreatePlayers(playerCount);
		}

		private List<Player> CreatePlayers(int playerCount)
		{
			return _playerFactory.CreatePlayers(playerCount);
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
				Player roundWinner = null;
				var roundCards = new List<Card>();
				while (roundWinner == null)
				{
					roundWinner = FindRoundWinner(roundCards);
					if (roundWinner == null)
					{
						var drawnPlayers = _drawService.FindDrawnPlayers(Players);
						_outputProvider("There was a draw");
						_warService.GoToWar(roundCards, drawnPlayers, CardsDealtInWar);
					}
				}

				RoundWonProcedure(roundWinner, roundCards);
			}

			var finalWinner = _winnerService.DetermineFinalWinner(Players);;
			_outputProvider($"{finalWinner} won with a total of {finalWinner.Score} wins" +
			                $" and a hand of {finalWinner.Hand.Count} cards");
		}

		private Player FindRoundWinner(List<Card> roundCards)
		{
			Player roundWinner;
			AddRoundCards(roundCards, Players);
			roundWinner = _winnerService.DetermineWinner(Players);
			OutputCardsDrawn(Players);
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

		private static void AddRoundCards(List<Card> roundCards, IEnumerable<Player> players)
		{
			foreach (var player in players)
			{
				roundCards.Add(player.DrawCard());
			}
		}

		private bool ReachedMinimumHandCount(List<Player> players)
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