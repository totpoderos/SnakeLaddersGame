namespace SnakeLaddersApi.Domain
{
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

        public override string ToString()
        {
            return $"{_position}";
        }

        public Position Increment(int spaces, int totalSpaces)
        {
            return _position + spaces > totalSpaces ? this : 
                new Position(_position + spaces);
        }
    }
}