using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerBoosts : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;

        private ActiveBoost _activeBoost;
        private IBoostsHandler[] _boostsHandlers;

        protected void Start()
        {
            FindHandlers();
        }

        private void FindHandlers()
        {
            var handlers = new LinkedList<IBoostsHandler>();

            foreach (MonoBehaviour behaviour in FindObjectsOfType<MonoBehaviour>())
            {
                if (behaviour is IBoostsHandler handler)
                    handlers.AddLast(handler);
            }
            _boostsHandlers = handlers.ToArray();
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (_activeBoost == null && collision.TryGetComponent(out Boost boost))
            {
                _activeBoost = boost.ApplyBoost(
                    new BoostArguments(_movement));

                NotifyHandlers(boost.NotifyHandler);
                _activeBoost.Ended.AddListener(BoostEnded);

                Destroy(boost.gameObject);
            }
        }

        protected void Update()
        {
            _activeBoost?.Tick();
        }

        private void BoostEnded()
        {
            _movement.SpeedMultiplier = 1f;

            NotifyHandlers(x => x?.OnBoostReset());
            _activeBoost = null;
        }

        private void NotifyHandlers(Action<IBoostsHandler> action)
        {
            var handlers = new Span<IBoostsHandler>(_boostsHandlers);

            for (int i = 0; i < handlers.Length; i++)
            {
                action(handlers[i]);
            }
        }
    }
}
