using System;
using System.Collections.Generic;

namespace YX
{
    public class Event : IEvent
    {
        private readonly LinkedList<Action> _listeners = new();

        public void AddListener(Action listener) => _listeners.AddLast(listener);

        public void RemoveListener(Action listener) => _listeners.Remove(listener);

        public void Invoke()
        {
            foreach (Action listener in _listeners)
                listener?.Invoke();
        }
    }

    public interface IEvent
    {
        public void AddListener(Action listener);
        public void RemoveListener(Action listener);
    }
}
