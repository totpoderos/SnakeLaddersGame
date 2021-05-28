using System;
using System.Collections.Generic;
using SnakeLaddersApi;
using Xunit;

namespace SnakeLaddersGameTests
{
    public class SnakeLaddersControllerShould
    {
        [Fact]
        public void PrintInitialPositionWhenPlayerPlacesTheToken()
        {
            var console = new InputOutputConsoleTestDouble("PlaceToken");
            var gameController = new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))), console);

            gameController.ProcessCommand();
            
            Assert.Equal("Position: 1", console.MessagePrinted);
        }
    }

    public class InputOutputConsoleTestDouble : InputOutputConsole
    {
        private readonly string _input;
        private string _output;

        public InputOutputConsoleTestDouble(string input)
        {
            _input = input;
        }
        public override string Read()
        {
            return _input;
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

        public SnakeLaddersController(Game game, InputOutputConsole inputOutputConsole)
        {
            _game = game;
            _inputOutputConsole = inputOutputConsole;
        }

        public void ProcessCommand()
        {
            if (_inputOutputConsole.Read().ToLower() == "placetoken")
            {
                _inputOutputConsole.Print($"Position: {_game.TokenPosition.ToString()}");
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