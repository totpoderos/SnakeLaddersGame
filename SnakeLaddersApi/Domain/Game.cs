namespace SnakeLaddersApi.Domain
{
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