using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Boost : MonoBehaviour
    {
        public abstract ActiveBoost ApplyBoost(BoostArguments arguments);

        public abstract void NotifyHandler(IBoostsHandler handler);
    }
}
