namespace Game
{
    public class SpeedUpBoost : Boost
    {
        public override ActiveBoost ApplyBoost(BoostArguments arguments)
        {
            var result = new ActiveBoost(duration: 5f);

            arguments.playerMovement.SpeedMultiplier *= 1.5f;
            result.Ended.AddListener(delegate
            {
                arguments.playerMovement.SpeedMultiplier /= 1.5f;
            });

            return result;
        }

        public override void NotifyHandler(IBoostsHandler handler)
            => handler?.OnSpeedUp();
    }
}
