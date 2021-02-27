using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using GameOfWar.Domain.Entities;
using GameOfWar.Domain.Enums;
using GameOfWar.Domain.Services;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using Xunit;

namespace GameOfWar.Domain.UnitTests
{
	public class WinnerServiceTests
	{
		// system under tests
		private readonly WinnerService _sut;

		public WinnerServiceTests()
		{
			_sut = new WinnerService();
		}
		
		[Theory]
		[MemberData(nameof(ShouldReturnHigherScore_TestData))]
		public void DetermineWinner_ShouldReturnHigherScorer(
			string expectedWinner, Card card1, Card card2)
		{
			// Arrange
			var player1 = new Player("1")
			{
				CurrentCard = card1
			};
			var player2 = new Player("2")
			{
				CurrentCard = card2
			};
			// Act
			var winner = _sut.DetermineWinner(player1, player2);
			// Assert
			Assert.Equal(expectedWinner, winner.Name);
		}

		[Theory]
		[MemberData(nameof(ShouldReturnNull_TestData))]
		public void DetermineWinner_ShouldReturnNull_WhenTie(Card card1, Card card2)
		{
			// Arrange
			var player1 = new Player("1")
			{
				CurrentCard = card1
			};
			var player2 = new Player("2")
			{
				CurrentCard = card2
			};
			// Act
			var winner = _sut.DetermineWinner(player1, player2);
			// Assert
			Assert.Null(winner);
		}
		
		
		public static IEnumerable<object[]> ShouldReturnHigherScore_TestData()
		{
			yield return new object[] {"2", new Card(Suit.Clubs, 1), new Card(Suit.Hearts, 8)};
			yield return new object[] {"1", new Card(Suit.Spades, 5), new Card(Suit.Clubs, 3)};
			yield return new object[] {"1", new Card(Suit.Diamonds, 13), new Card(Suit.Clubs, 10)};
			yield return new object[] {"2", new Card(Suit.Spades, 1), new Card(Suit.Diamonds, 5)};
		}

		public static IEnumerable<object[]> ShouldReturnNull_TestData()
		{
			yield return new object[] { new Card(Suit.Clubs, 3), new Card(Suit.Diamonds, 3) };
			yield return new object[] { new Card(Suit.Hearts, 1), new Card(Suit.Clubs, 1) };
			yield return new object[] { new Card(Suit.Spades, 4), new Card(Suit.Diamonds, 4) };
			yield return new object[] { new Card(Suit.Hearts, 5), new Card(Suit.Hearts, 5) };
		}
	}
}