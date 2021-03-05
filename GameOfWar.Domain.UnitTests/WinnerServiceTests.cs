using System.Collections.Generic;
using GameOfWar.Domain.Entities;
using GameOfWar.Domain.Enums;
using GameOfWar.Domain.Services;
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

		[Theory]
		[MemberData(nameof(ShouldReturnHighestScore_TestData))]
		public void DetermineWinner_ShouldReturnHighestScore_WhenValidInputNoDuplicates(List<int> ranks, int largest)
		{
			// Arrange
			var players = new List<Player>(ranks.Count);
			foreach (var rank in ranks)
			{
				players.Add(
					new Player(rank.ToString())
					{
						CurrentCard = new Card(Suit.Clubs, rank)
					});
			}
			var expected = players[ranks.IndexOf(largest)];
			// Act
			var winner = _sut.DetermineWinner(players);
			// Assert
			Assert.Equal(expected, winner);
		}

		[Theory]
		[MemberData(nameof(ShouldReturnNullHighestDuplicates_TestData))]
		public void DetermineWinner_ShouldReturnNull_WhenHighestAreDuplicatesInput(List<int> ranks)
		{
			// Arrange
			var players = new List<Player>(ranks.Count);
			foreach (var rank in ranks)
			{
				players.Add(
					new Player(rank.ToString())
					{
						CurrentCard = new Card(Suit.Clubs, rank)
					});
			}
			// Act
			var winner = _sut.DetermineWinner(players);
			// Assert
			Assert.Null(winner);
		}
		
		[Theory]
		[MemberData(nameof(ShouldReturnHighest_WhenNonHighestDuplicates_TestData))]
		public void DetermineWinner_ShouldReturnHighest_WhenNonHighestAreDuplicatesInput(List<int> ranks, int largest)
		{
			// Arrange
			var players = new List<Player>(ranks.Count);
			foreach (var rank in ranks)
			{
				players.Add(
					new Player(rank.ToString())
					{
						CurrentCard = new Card(Suit.Clubs, rank)
					});
			}
			var expected = players[ranks.IndexOf(largest)];
			// Act
			var winner = _sut.DetermineWinner(players);
			// Assert
			Assert.Equal(expected, winner);
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

		public static IEnumerable<object[]> ShouldReturnHighestScore_TestData()
		{
			yield return new object[] { new List<int>() { 1, 2, 3, 4 }, 4 };
			yield return new object[] { new List<int>() { 2, 3, 6, 1 }, 6 };
			yield return new object[] { new List<int>() { 10, 3, 2, 1 }, 10 };
			yield return new object[] { new List<int>() { 1 }, 1 };
			yield return new object[] { new List<int>() { 1, 10 }, 10 };
			yield return new object[] { new List<int>() { 3, 6, 10, 32, 64, 12, 34 }, 64 };
		}
		
		public static IEnumerable<object[]> ShouldReturnNullHighestDuplicates_TestData()
		{
			yield return new object[] { new List<int>() { 1, 3, 4, 4 }};
			yield return new object[] { new List<int>() { 1, 5, 4, 3, 2, 6, 7, 7 }};
			yield return new object[] { new List<int>() { 7 ,7, 3, 1, 2, 1 }};
			yield return new object[] { new List<int>() { 100, 2 , 2, 3, 2, 100 }};
			yield return new object[] { new List<int>() { 100, 2 , 2, 3, 2, 100, 100, 100 }};
		}
		
		public static IEnumerable<object[]> ShouldReturnHighest_WhenNonHighestDuplicates_TestData()
		{
			yield return new object[] { new List<int>() { 1, 1, 3, 4 }, 4 };
			yield return new object[] { new List<int>() { 3, 3, 6, 1 }, 6 };
			yield return new object[] { new List<int>() { 10, 2, 2, 2 }, 10 };
			yield return new object[] { new List<int>() { 1 }, 1 };
			yield return new object[] { new List<int>() { 1, 1, 2, 3, 3, 10 }, 10 };
			yield return new object[] { new List<int>() { 3, 6, 10, 32, 32, 64, 12, 34 }, 64 };
		}
	}
}