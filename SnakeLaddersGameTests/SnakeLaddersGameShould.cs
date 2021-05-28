using SnakeLaddersGame;
using Xunit;

namespace SnakeLaddersGameTests
{
    public class SnakeLaddersGameShould
    {
        [Fact]
        public void foo()
        {
            var game = new Game();
            var expectedPosition = new Position(1);
            
            Assert.Equal(expectedPosition, game.TokenPosition);
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

    }

    public class Game
    {
        private Position _position;

        public Game()
        {
            _position = new Position(1);
        }

        public Position TokenPosition => _position;
    }
}