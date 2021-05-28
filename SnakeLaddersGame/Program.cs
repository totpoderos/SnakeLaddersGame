using System;
using SnakeLaddersApi;
using SnakeLaddersApi.Domain;

namespace SnakeLaddersGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var snakeLaddersController =
                new SnakeLaddersController(new Game(new OneToSixDice(new RandomGenerator(1, 6))),
                    new InputOutputConsole());
            Console.WriteLine("Welcome");
            Console.WriteLine("Available commands");
            Console.WriteLine("PlaceToken: Place token at starting position");
            Console.WriteLine("Print:      Print current position");
            Console.WriteLine("Move n:     Move token n spaces");
            Console.WriteLine("RollDice:   Move token using roll");
            Console.WriteLine("Status:     Check if player has won");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Command:> ");
                snakeLaddersController.ProcessCommand();
            }
        }
    }
}