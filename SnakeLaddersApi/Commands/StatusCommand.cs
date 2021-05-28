namespace SnakeLaddersApi.Commands
{
    public class StatusCommand : Command
    {
        private readonly SnakeLaddersController _snakeLaddersController;

        public StatusCommand(SnakeLaddersController snakeLaddersController)
        {
            _snakeLaddersController = snakeLaddersController;
        }

        public override void Execute()
        {
            _snakeLaddersController.Status();
        }
    }
}