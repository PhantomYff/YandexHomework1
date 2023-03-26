using System;
using UnityEngine;

namespace Game
{
    public class ActiveBoost
    {
        public ActiveBoost(Action<ActiveBoost> endedCallback, float duration)
        {
            _endedCallback = endedCallback;
            _duration = duration;
        }

        private float _duration;

        private readonly Action<ActiveBoost> _endedCallback;

        public void Tick()
        {
            _duration -= Time.deltaTime;

            if (_duration <= 0)
            {
                _endedCallback.Invoke(this);
            }
        }
    }
}
