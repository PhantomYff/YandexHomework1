using UnityEngine;
using YX;

using Event = YX.Event;

namespace Game
{
    public class ActiveBoost
    {
        public IEvent Ended => _ended;

        private readonly Event _ended = new();

        public ActiveBoost(float duration)
        {
            _duration = duration;
        }

        private float _duration;

        public void Tick()
        {
            _duration -= Time.deltaTime;

            if (_duration <= 0)
            {
                _ended.Invoke();
            }
        }
    }
}
