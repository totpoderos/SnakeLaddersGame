using System;
using SnakeLaddersApi.Domain;
using Xunit;

namespace SnakeLaddersGameTests
{
    public class SnakeLaddersGameShould
    {
        [Fact]
        public void MoveTokenToFirstPositionWhenGameStarts()
        {
            var game = new Game();

            Assert.Equal(new Position(1), game.TokenPosition);
        }

        [Fact]
        public void MoveTokenThreeSpacesToPositionFour()
        {
            var game = new Game();

            game.Move(3);
            
            Assert.Equal(new Position(4), game.TokenPosition);
        }

        [Fact]
        public void MakeToMovesWithTokenToFinalPosition()
        {
            var game = new Game();
            
            game.Move(3);
            game.Move(4);
            
            Assert.Equal(new Position(8), game.TokenPosition);
        }
        
        [Fact]
        public void WinTheGameWhenReachingFinalPosition()
        {
            var game = new Game();
            game.Move(96);
            
            game.Move(3);
            
            Assert.Equal(new Position(100), game.TokenPosition);
            Assert.True(game.IsWon);
        }
        
        [Fact]
        public void NotWinTheGameWhenLastMoveGoesBeyondFinalPosition()
        {
            var game = new Game();
            game.Move(96);
            
            game.Move(4);
            
            Assert.Equal(new Position(97), game.TokenPosition);
            Assert.False(game.IsWon);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        public void RaiseErrorWhenDiceRollNumberIsOutOfBounds(int diceNumber)
        {
            var game = new Game(new OneToSixDice(new RandomGeneratorTestDouble(1, 6, diceNumber)));

            Assert.Throws<Exception>(() => game.RollDice());
        }

        [Fact]
        public void MoveFourSpacesWhenRollinTheDice()
        {
            var game = new Game(new OneToSixDice(new RandomGeneratorTestDouble(1, 6, 4)));
            
            game.Move(game.RollDice());
            
            Assert.Equal(new Position(5), game.TokenPosition);
        }
    }

    public class RandomGeneratorTestDouble : RandomGenerator
    {
        private readonly int _result;

        public RandomGeneratorTestDouble(int start, int end, int result): base(start, end)
        {
            _result = result;
        }

        public override int RandomNumber()
        {
            return _result;
        }
    }
}