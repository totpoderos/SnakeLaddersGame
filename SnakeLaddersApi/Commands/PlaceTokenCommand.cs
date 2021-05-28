namespace SnakeLaddersApi.Commands
{
    public class PlaceTokenCommand : Command
    {
        private readonly SnakeLaddersController _snakeLaddersController;

        public PlaceTokenCommand(SnakeLaddersController snakeLaddersController)
        {
            _snakeLaddersController = snakeLaddersController;
        }

        public override void Execute()
        {
            _snakeLaddersController.PlaceToken();
        }
    }
}