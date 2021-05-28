using SnakeLaddersApi;
using SnakeLaddersApi.Domain;
using Xunit;

namespace SnakeLaddersGameTests
{
    public class SnakeLaddersControllerShould
    {
        private readonly InputOutputConsoleTestDouble _console;
        private readonly SnakeLaddersController _gameController;

        public SnakeLaddersControllerShould()
        {
            _console = new InputOutputConsoleTestDouble();
            _gameController = new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))), _console);
        }
        
        [Fact]
        public void PrintInitialPositionWhenPlayerPlacesTheToken()
        {
            _console.ReadWillReturn("PlaceToken");

            _gameController.ProcessCommand();
            
            Assert.Equal("Position: 1", _console.MessagePrinted);
        }        
        
        
        [Fact]
        public void ShouldNotProcessAnyCommandIfTokenIsNotPlaced()
        {
            _console.ReadWillReturn("Print");
            
            _gameController.ProcessCommand();
            
            Assert.Equal("Token is not placed", _console.MessagePrinted);
        }

        [Fact]
        public void PrintCurrentPosition()
        {
            _console.ReadWillReturn("PlaceToken");
            _gameController.ProcessCommand();
            _console.ReadWillReturn("Print");
            
            _gameController.ProcessCommand();
            
            Assert.Equal("Position: 1", _console.MessagePrinted);
        }

        [Fact]
        public void ProcessMoveCommand()
        {
            _console.ReadWillReturn("PlaceToken");
            _gameController.ProcessCommand();
            _console.ReadWillReturn("Move 3");
            _gameController.ProcessCommand();
            _console.ReadWillReturn("Print");
            
            _gameController.ProcessCommand();
            
            Assert.Equal("Position: 4", _console.MessagePrinted);
        }

        [Fact]
        public void ProcessRollDiceCommand()
        {
            _console.ReadWillReturn("PlaceToken");
            _gameController.ProcessCommand();
            
            _console.ReadWillReturn("RollDice");
            _gameController.ProcessCommand();
            
            Assert.Contains("You get a: ", _console.MessagePrinted);
        }

        [Fact]
        public void WinGame()
        {
            _console.ReadWillReturn("PlaceToken");
            _gameController.ProcessCommand();
            
            _console.ReadWillReturn("Move 99");
            _gameController.ProcessCommand();
            
            _console.ReadWillReturn("Status");
            _gameController.ProcessCommand();
            
            Assert.Equal("You win the game!", _console.MessagePrinted);
        }

        [Fact]
        public void PlayerHasNotWonTheGame()
        {
            _console.ReadWillReturn("PlaceToken");
            _gameController.ProcessCommand();
            
            _console.ReadWillReturn("Move 2");
            _gameController.ProcessCommand();
            
            _console.ReadWillReturn("Status");
            _gameController.ProcessCommand();
            
            Assert.Equal("You didn't win the game", _console.MessagePrinted);
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
}