namespace SnakeLaddersApi.Commands
{
    public class MoveCommand : Command
    {
        private readonly SnakeLaddersController _snakeLaddersController;
        private readonly int _spaces;

        public MoveCommand(SnakeLaddersController snakeLaddersController, int spaces)
        {
            _snakeLaddersController = snakeLaddersController;
            _spaces = spaces;
        }

        public override void Execute()
        {
            _snakeLaddersController.Move(_spaces);
        }
    }
}