using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Game
{
    public class WeightedRandom<T> where T : IWeighted
    {
        public WeightedRandom(IReadOnlyList<T> values)
        {
            _values = values;
        }

        public WeightedRandom(IEnumerable<T> values) : this(values.ToArray()) { }

        public IReadOnlyList<T> Values => _values;

        private T _lastResult;
        private bool _used;

        private readonly IReadOnlyList<T> _values;

        public T GetRandomValue(float t)
        {
            float weightsSum = 0;

            float[] weights = _values
                .Select(x => Mathf.Abs(x.GetWeight(t)))
                .ToArray();

            var randomWeight = Random.Range(0f, weights.Sum());

            for (var i = 0; i < _values.Count; i++)
            {
                weightsSum += weights[i];

                if (randomWeight <= weightsSum)
                {
                    if (_used &&
                        _lastResult?.GetHashCode() == _values[i]?.GetHashCode() &&
                        _lastResult?.Equals(_values[i]) != false)
                    {
                        return GetRandomValue(t);
                    }

                    _used = true;
                    _lastResult = _values[i];
                    return _values[i];
                }
            }
            throw new UnityException("Couldn't get random value.");
        }
    }
}
