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

        public Position Increment(int spaces)
        {
            return new Position(_position + spaces);
        }
    }

    public class Game
    {
        private Position _position;

        public Game()
        {
            _position = new Position(1);
        }

        public void Move(int spaces)
        {
            _position = _position.Increment(spaces);
            
        }
        public Position TokenPosition => _position;
        public bool IsWon => _position.Equals(new Position(100));

    }
}