using UnityEngine;
using UnityEngine.Events;
using YX.KeyboardBinding;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        public float SpeedMultiplier { get; set; } = 1f;
        public bool IsNoClip { get; set; }

        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _wallLayer;

        [SerializeField] private UnityEvent<Vector2> _started;
        [SerializeField] private UnityEvent _ended;
        [SerializeField] private UnityEvent<float> _moving;
        [SerializeField] private UnityEvent _afterMovement;

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
            _afterMovement.Invoke();
        }

        public void StopMovement()
        {
            if (enabled == false || CanPassInDirection(Vector2.zero) == false)
                return;

            transform.position = Vector3Int.RoundToInt(transform.position);
            _ended.Invoke();

            _timeMoving = 0;
            _direction = Vector3.zero;
        }

        private void SetMovement(Vector2Int newDirection)
        {
            if (newDirection == Vector2Int.zero)
            {
                StopMovement();
                return;
            }

            if (enabled == false)
                return;

            if (_direction == Vector3.zero && 
                (CanPassInDirection(newDirection) || CanPassInDirection(Vector2.zero) == false))
            {
                _started.Invoke(newDirection);

                _timeMoving = 0;
                _direction = (Vector2)newDirection;
            }
        }

        private bool CanPassInDirection(Vector2 direction)
        {
            var collider = Physics2D.OverlapPoint((Vector2)transform.position + direction, _wallLayer);

            if (collider && collider.TryGetComponent(out Wall wall))
            {
                return wall.AllowNoClip && IsNoClip;
            }
            return true;
        }
    }
}
