namespace SnakeLaddersApi.Commands
{
    public class PrintCommand : Command
    {
        private readonly SnakeLaddersController _snakeLaddersController;

        public PrintCommand(SnakeLaddersController snakeLaddersController)
        {
            _snakeLaddersController = snakeLaddersController;
        }

        public override void Execute()
        {
            _snakeLaddersController.Print();
        }
    }
}