using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Game
{
    public class LevelPiece : MonoBehaviour, IWeighted
    {
        public event Action<LevelPiece> EndReached;

        [field: SerializeField] public int StartPosition { get; private set; }
        [field: SerializeField] public int EndPosition { get; private set; }

        [Space(10f)]
        [SerializeField] private AnimationCurve _weightCurve;
        [SerializeField] private float _weightMultiplier = 1;

        [Space(10f)]
        [SerializeField] private GameObject[] _variations;
        [SerializeField] private AnimationCurve _variationChanceCurve;

        public float GetWeight(float t)
            => _weightCurve.Evaluate(t) * _weightMultiplier;

        public void Init(int piecesGenerated)
        {
            EnableVariation(piecesGenerated);
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerMovement player))
                EndReached?.Invoke(this);
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + new Vector3(StartPosition, 0), Vector3.one);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + new Vector3(EndPosition, 0), Vector3.one);
        }

        public override int GetHashCode() => GetInstanceID();

        private void EnableVariation(int pieceGenerated)
        {
            if (_variations.Length == 0)
                return;

            var chance = Mathf.Clamp01(_variationChanceCurve.Evaluate(pieceGenerated));

            if (Random.Range(0f, 1f) <= chance)
            {
                int randomIndex = Random.Range(0, _variations.Length);

                _variations[randomIndex].SetActive(true);

                int i = 0;
                for (; i < randomIndex; i++)
                    Destroy(_variations[i]);

                i++;
                for (; i < _variations.Length; i++)
                    Destroy(_variations[i]);
            }
        }
    }
}
