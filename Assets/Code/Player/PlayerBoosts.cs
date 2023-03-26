using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerBoosts : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerVisuals _visuals;

        private ActiveBoost _speedUpBoost;

        public IEnumerable<ActiveBoost> Boosts
        {
            get
            {
                yield return _speedUpBoost;
            }
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Boost boost))
            {
                switch (boost.Type)
                {
                    case BoostType.SpeedUp:
                        _movement.SpeedMultiplier = 1.5f;
                        _visuals.SetSpeedUpColor();

                        _speedUpBoost = new ActiveBoost(SpeedBoostEnded, 5f);
                        break;
                }

                Destroy(boost.gameObject);
            }
        }

        protected void Update()
        {
            foreach (ActiveBoost boost in Boosts)
            {
                boost?.Tick();
            }
        }

        private void SpeedBoostEnded(ActiveBoost boost)
        {
            _movement.SpeedMultiplier = 1f;
            _visuals.SetDefaultColor();

            _speedUpBoost = null;
        }
    }
}
