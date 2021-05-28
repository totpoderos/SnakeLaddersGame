using System;
using SnakeLaddersGame;
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

    public class OneToSixDice
    {
        private readonly RandomGenerator _randomGenerator;

        public OneToSixDice(RandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public int Roll()
        {
            var number = _randomGenerator.RandomNumber();
            if (number < 1 || number > 6) throw new Exception("Out of range dice number");
            return number;
        }
    }

    public class RandomGenerator
    {
        private readonly int _start;
        private readonly int _end;

        public RandomGenerator(int start, int end)
        {
            _start = start;
            _end = end;
        }

        public virtual int RandomNumber()
        {
            throw new NotImplementedException();
        }
    }

    public class Position
    {
        private int _position;
        
        public Position(int initialPosition)
        {
            _position = initialPosition;
        }
        
        protected bool Equals(Position other)
        {
            return _position == other._position;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            return _position;
        }

        public Position Increment(int spaces, int totalSpaces)
        {
            return _position + spaces > totalSpaces ? this : 
                new Position(_position + spaces);
        }
    }

    public class Game
    {
        private const int TotalSpaces = 100;
        private readonly OneToSixDice _oneToSixDice;
        private Position _position;

        public Game()
        {
            _position = new Position(1);
        }
        public Game(OneToSixDice oneToSixDice) : this()
        {
            _oneToSixDice = oneToSixDice;
        }

        public void Move(int spaces)
        {
            _position = _position.Increment(spaces, TotalSpaces);
            
        }
        public Position TokenPosition => _position;
        public bool IsWon => _position.Equals(new Position(100));

        public int RollDice()
        {
            return _oneToSixDice.Roll();
        }
    }
}