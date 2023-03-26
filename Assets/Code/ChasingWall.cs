using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class ChasingWall : MonoBehaviour
    {
        [SerializeField] private LevelGenerator _generator;
        [SerializeField] private AnimationCurve _speed;
        [SerializeField] private float _speedModifier;
        [SerializeField] private UnityEvent _playerTouched;

        protected void Update()
        {
            float speed = _speed.Evaluate(_generator.PiecesGenerated) * _speedModifier;
            transform.position += speed * Time.deltaTime * Vector3.right;

            if (_generator.PiecesGenerated >= _generator.MaxPiecesCount)
            {
                LevelPiece leftBoarder = _generator.Level.First.Value;
                float leftBorderX = leftBoarder.transform.position.x + leftBoarder.StartPosition;

                if (leftBorderX > transform.position.x)
                {
                    transform.position = Vector3.right * leftBorderX;
                }
            }
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<PlayerMovement>(out _))
            {
                _playerTouched.Invoke();
            }
        }
    }
}
