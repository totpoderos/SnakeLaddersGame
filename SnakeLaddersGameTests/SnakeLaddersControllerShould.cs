using System;
using SnakeLaddersApi;
using Xunit;

namespace SnakeLaddersGameTests
{
    public class SnakeLaddersControllerShould
    {
        [Fact]
        public void PrintInitialPositionWhenPlayerPlacesTheToken()
        {
            var console = new InputOutputConsoleTestDouble();
            var gameController = new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))), console);
            console.ReadWillReturn("PlaceToken");

            gameController.ProcessCommand();
            
            Assert.Equal("Position: 1", console.MessagePrinted);
        }        
        
        
        [Fact]
        public void ShouldNotProcessAnyCommandIfTokenIsNotPlaced()
        {
            var console = new InputOutputConsoleTestDouble();
            var gameController = new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))), console);
            console.ReadWillReturn("Print");
            
            gameController.ProcessCommand();
            
            Assert.Equal("Token is not placed", console.MessagePrinted);
        }

        [Fact]
        public void PrintCurrentPosition()
        {
            var console = new InputOutputConsoleTestDouble();
            var gameController = new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))), console);
            console.ReadWillReturn("PlaceToken");
            gameController.ProcessCommand();
            console.ReadWillReturn("Print");
            
            gameController.ProcessCommand();
            
            Assert.Equal("Position: 1", console.MessagePrinted);
        }

        [Fact]
        public void ProcessMoveCommand()
        {
            var console = new InputOutputConsoleTestDouble();
            var gameController = new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))), console);
            console.ReadWillReturn("PlaceToken");
            gameController.ProcessCommand();
            console.ReadWillReturn("Move 3");
            gameController.ProcessCommand();
            console.ReadWillReturn("Print");
            
            gameController.ProcessCommand();
            
            Assert.Equal("Position: 4", console.MessagePrinted);
        }

        [Fact]
        public void ProcessRollDiceCommand()
        {
            var console = new InputOutputConsoleTestDouble();
            var gameController = new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))), console);
            console.ReadWillReturn("PlaceToken");
            gameController.ProcessCommand();
            
            console.ReadWillReturn("RollDice");
            gameController.ProcessCommand();
            
            Assert.Contains("You get a: ", console.MessagePrinted);
        }

        [Fact]
        public void WinGame()
        {
            var console = new InputOutputConsoleTestDouble();
            var gameController = new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))), console);
            console.ReadWillReturn("PlaceToken");
            gameController.ProcessCommand();
            
            console.ReadWillReturn("Move 99");
            gameController.ProcessCommand();
            
            console.ReadWillReturn("Status");
            gameController.ProcessCommand();
            
            Assert.Equal("You win the game!", console.MessagePrinted);
        }

        [Fact]
        public void PlayerHasNotWonTheGame()
        {
            var console = new InputOutputConsoleTestDouble();
            var gameController = new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))), console);
            console.ReadWillReturn("PlaceToken");
            gameController.ProcessCommand();
            
            console.ReadWillReturn("Move 2");
            gameController.ProcessCommand();
            
            console.ReadWillReturn("Status");
            gameController.ProcessCommand();
            
            Assert.Equal("You didn't win the game", console.MessagePrinted);
        }
    }

    public class InputOutputConsoleTestDouble : InputOutputConsole
    {
        private string _input;
        private string _output;

        public override string Read()
        {
            return _input;
        }

        public void ReadWillReturn(string input)
        {
            _input = input;
        }

        public override void Print(string output)
        {
            _output = output;
        }

        public string MessagePrinted => _output;
    }

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
            var command = _inputOutputConsole.Read().ToLower();
            if (command == "placetoken")
            {
                _tokenPlaced = true;
                _inputOutputConsole.Print($"Position: {_game.TokenPosition}");
            }

            if (!_tokenPlaced)
            {
                _inputOutputConsole.Print("Token is not placed");
                return;
            }
            
            if (command == "print")
            {
                _inputOutputConsole.Print($"Position: {_game.TokenPosition}");
            }
            
            if (command.Contains("move"))
            {
                var spaces = command.Replace("move", "").Trim();
                _game.Move(Int32.Parse(spaces));
            }

            if (command == "rolldice")
            {
                int spaces = _game.RollDice();
                _game.Move(spaces);
                _inputOutputConsole.Print($"You get a: {spaces}");
            }
            
            if (command == "status")
            {
                if (_game.IsWon)
                    _inputOutputConsole.Print("You win the game!");
                else 
                    _inputOutputConsole.Print("You didn't win the game");
            }
        }
    }

    public class InputOutputConsole
    {
        public virtual void Print(string output)
        {
            throw new NotImplementedException();
        }

        public virtual string Read()
        {
            throw new NotImplementedException();
        }
    }
}