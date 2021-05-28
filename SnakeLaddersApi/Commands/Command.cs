namespace SnakeLaddersApi.Commands
{
    public abstract class Command
    {
        private const string PlaceToken = "placetoken";
        private const string Print = "print";
        private const string Move = "move";
        private const string RollDice = "rolldice";
        private const string Status = "status";
        
        public static Command Create(string input, SnakeLaddersController snakeLaddersController)
        {
            if (input == PlaceToken)
                return new PlaceTokenCommand(snakeLaddersController);
            if (input == Print)
                return new PrintCommand(snakeLaddersController);
            
            if (input.Contains(Move))
            {
                var spaces = input.Replace("move", "").Trim();
                return new MoveCommand(snakeLaddersController, int.Parse(spaces));
            }

            if (input == RollDice)
                return new RollDiceCommand(snakeLaddersController);

            if (input == Status)
                return new StatusCommand(snakeLaddersController);

            return null;
        }

        public virtual void Execute() { }
    }
}