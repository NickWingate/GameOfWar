using System;
using System.Collections;
using System.Collections.Generic;
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

		public Game(
			IDealCardsService dealCardsService, 
			IWinnerService winnerService,
			Func<string> inputProvider,
			Action<string> outputProvider,
			int playerCount)
		{
			_dealCardsService = dealCardsService;
			_winnerService = winnerService;
			_inputProvider = inputProvider;
			_outputProvider = outputProvider;
			Players = CreatePlayers(playerCount);
		}

		private List<Player> CreatePlayers(int playerCount)
		{
			List<Player> players = new List<Player>(playerCount);
			for (int i = 0; i < playerCount; i++)
			{
				_outputProvider($"Name for player {i+1}: ");
				var name = _inputProvider();
				players.Add(new Player(name));
			}

			return players;
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
			Player roundWinner = null;
			var roundCards = new List<Card>();
			while (!ReachedMinimumHandCount(Players))
			{
				while (roundWinner == null)
				{
					AddRoundCards(roundCards, Players);
					roundWinner = _winnerService.DetermineWinner(Players);
					OutputCardsDrawn(Players);
					if (roundWinner == null)
					{
						_outputProvider("There was a draw");
						GoToWar(roundCards, Players, CardsDealtInWar);
					}
				}

				roundWinner.Score++;
				roundWinner.Hand.AddRange(roundCards);
				_outputProvider($"{roundWinner} won this round, they gained {roundCards.Count} cards and have " +
				                $"{roundWinner.Score} wins and {roundWinner.Hand.Count} cards in their hand");
				roundCards.Clear();
				roundWinner = null;
				_inputProvider();
			}

			var finalWinner = FinalWinner(Players);
			_outputProvider($"{finalWinner} won with a total of {finalWinner.Score} wins" +
			                $" and a hand of {finalWinner.Hand.Count} cards");
		}

		private void GoToWar(ICollection<Card> roundCards, IEnumerable<Player> players, int cardsToAdd)
		{
			foreach (var player in players)
			{
				for (int i = 0; i < cardsToAdd; i++)
				{
					roundCards.Add(player.DrawCard());
				}
			}
		}

		private Player FinalWinner(List<Player> players)
		{
			Player winner = players[0];
			foreach (var player in players)
			{
				if (player.Hand.Count < winner.Hand.Count)
				{
					winner = player;
				}
			}

			return winner;
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