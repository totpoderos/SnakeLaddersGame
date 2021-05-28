namespace SnakeLaddersApi.Commands
{
    public class RollDiceCommand : Command
    {
        private readonly SnakeLaddersController _snakeLaddersController;

        public RollDiceCommand(SnakeLaddersController snakeLaddersController)
        {
            _snakeLaddersController = snakeLaddersController;
        }

        public override void Execute()
        {
            _snakeLaddersController.RollDice();
        }
    }
}