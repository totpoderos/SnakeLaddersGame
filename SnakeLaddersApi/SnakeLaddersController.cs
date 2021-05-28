using SnakeLaddersApi.Commands;
using SnakeLaddersApi.Domain;

namespace SnakeLaddersApi
{
    public class SnakeLaddersController
    {
        private readonly Game _game;
        private readonly InputOutputConsole _inputOutputConsole;
        private bool _tokenPlaced;

        public SnakeLaddersController(Game game, InputOutputConsole inputOutputConsole)
        {
            _game = game;
            _inputOutputConsole = inputOutputConsole;
        }

        public void ProcessCommand()
        {
            var input = _inputOutputConsole.Read().ToLower();
            Command command = Command.Create(input, this);
            command.Execute();
            if(!_tokenPlaced) _inputOutputConsole.Print("Token is not placed");
        }

        public void PlaceToken()
        {
            _tokenPlaced = true;
            _inputOutputConsole.Print($"Position: {_game.TokenPosition}");
        }

        public void Print()
        {
            if (!_tokenPlaced) return;
            _inputOutputConsole.Print($"Position: {_game.TokenPosition}");
        }

        public void Move(int spaces)
        {
            if (!_tokenPlaced) return;
            _game.Move(spaces);
        }

        public void RollDice()
        {
            if (!_tokenPlaced) return;
            int spaces = _game.RollDice();
            _game.Move(spaces);
            _inputOutputConsole.Print($"You get a: {spaces}");
        }

        public void Status()
        {
            if (!_tokenPlaced) return;
            if (_game.IsWon)
                _inputOutputConsole.Print("You win the game!");
            else 
                _inputOutputConsole.Print("You didn't win the game");
        }
    }
}