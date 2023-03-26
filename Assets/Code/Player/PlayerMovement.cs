using UnityEngine;
using UnityEngine.Events;
using YX.KeyboardBinding;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        public float SpeedMultiplier { get; set; } = 1f;

        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _wallLayer;

        [SerializeField] private UnityEvent<Vector2> _started;
        [SerializeField] private UnityEvent _ended;
        [SerializeField] private UnityEvent<float> _moving;

        private Vector3 _direction;
        private float _timeMoving;

        protected void Awake()
        {
            Keyboard.BindToAxis(Axis.Horizontal).Raw(x => SetMovement(new((int)x, 0)));
            Keyboard.BindToAxis(Axis.Vertical).Raw(x => SetMovement(new(0, (int)x)));
        }

        protected void FixedUpdate()
        {
            if (_direction != Vector3.zero)
            {
                transform.position += Time.fixedDeltaTime * _speed * SpeedMultiplier * _direction;

                _moving.Invoke(_timeMoving);
                _timeMoving += Time.deltaTime;
            }
        }

        public void StopMovement()
        {
            SetMovement(Vector2Int.zero);
        }

        private void SetMovement(Vector2Int direction)
        {
            if (enabled == false)
                return;

            if (direction == Vector2Int.zero)
            {
                transform.position = Vector3Int.RoundToInt(transform.position);
                _ended.Invoke();
            }
            else
            {
                if (_direction != Vector3.zero ||
                    Physics2D.OverlapPoint((Vector2)transform.position + direction, _wallLayer))
                    return;

                _started.Invoke(direction);
            }

            _timeMoving = 0;
            _direction = (Vector2)direction;
        }
    }
}
