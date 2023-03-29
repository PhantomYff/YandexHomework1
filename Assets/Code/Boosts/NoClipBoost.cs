namespace Game
{
    public class NoClipBoost : Boost
    {
        public override ActiveBoost ApplyBoost(BoostArguments arguments)
        {
            var result = new ActiveBoost(duration: 3f);

            arguments.playerMovement.IsNoClip = true;
            result.Ended.AddListener(delegate
            {
                arguments.playerMovement.IsNoClip = false;
            });

            return result;
        }

        public override void NotifyHandler(IBoostsHandler handler)
            => handler?.OnNoClip();
    }
}
