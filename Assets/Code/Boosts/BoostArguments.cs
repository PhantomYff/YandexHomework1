namespace Game
{
    public class BoostArguments
    {
        public BoostArguments(PlayerMovement playerMovement)
        {
            this.playerMovement = playerMovement;
        }

        public readonly PlayerMovement playerMovement;
    }
}
