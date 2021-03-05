using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GameOfWar.Application.Services;
using GameOfWar.Domain.Entities;
using GameOfWar.Domain.Enums;
using Xunit;

namespace GameOfWar.Application.UnitTests
{
	public class DrawServiceTests
	{
		private readonly IDrawService _sut;
		public DrawServiceTests()
		{
			_sut = new DrawService();
		}
		
		[Theory]
		[MemberData(nameof(ShouldReturnDrawnPlayers_TestData))]
		public void FindDrawnPlayers_ShouldReturnHighestDrawnPlayers(
			int[] ranks, int[] indexOfDrawnPlayers)
		{
			// Arrange
			var players = new List<Player>();
			foreach (var rank in ranks)
			{
				players.Add(new Player(rank.ToString())
				{
					CurrentCard = new Card(Suit.Clubs, rank)
				});
			}
			var expected = new List<Player>();
			foreach (var index in indexOfDrawnPlayers)
			{
				expected.Add(players[index]);
			}
			// Act
			var actual = _sut.FindDrawnPlayers(players);
			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(new int[] {10, 9, 9})]
		[InlineData(new int[] {3, 2, 2})]
		[InlineData(new int[] {1, 5, 1})]
		[InlineData(new int[] {1, 3, 8, 1 ,5, 6, 6 })]
		public void FindDrawnPlayers_ShouldThrowException_WhenHighestRankNotDrawn(int[] ranks)
		{
			// Arrange
			var players = new List<Player>();
			foreach (var rank in ranks)
			{
				players.Add(new Player(rank.ToString())
				{
					CurrentCard = new Card(Suit.Clubs, rank)
				});
			}
			// Act
			
			// Assert
			Assert.Throws<ArgumentException>(() => _sut.FindDrawnPlayers(players));
		}

		public static IEnumerable<object[]> ShouldReturnDrawnPlayers_TestData()
		{
			yield return new object[] { new int[] { 1, 2, 2 }, new int[] { 1, 2 } };
			yield return new object[] { new int[] { 1, 1, 2, 2 }, new int[] { 2, 3 } };
			yield return new object[] { new int[] { 10, 9, 8, 8, 9, 11, 10, 11 }, new int[] { 5, 7 } };
			yield return new object[] { new int[] { 10, 10, 10 }, new int[] { 0, 1, 2 } };
			yield return new object[] { new int[] { 1, 2, 2 }, new int[] { 1, 2 } };
		}
	}
}