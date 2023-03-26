using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerVisuals : MonoBehaviour
    {
        [Header("Moving Audio")]
        [SerializeField] private AudioSource _source;
        [SerializeField] private float _startPitch;
        [SerializeField] private float _pitchIncrement;
        [SerializeField] private float _soundInterval;
        [Header("Colors")]
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _speedUpColor;

        private float _soundTimes;
        private Animator _animator;
        private SpriteRenderer _sprite;

        private const string HORIZONTAL_MOVEMENT = "HorizontalMovement";
        private const string VERTICAL_MOVEMENT = "VerticalMovement";

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetSpeedUpColor()
            => _sprite.color = _speedUpColor;

        public void SetDefaultColor()
            => _sprite.color = _defaultColor;

        public void MovingStarted(Vector2 movement)
        {
            if (movement.x != 0)
            {
                _animator.SetFloat(HORIZONTAL_MOVEMENT, Mathf.Abs(movement.x));
            }
            else if (movement.y != 0)
            {
                _animator.SetFloat(VERTICAL_MOVEMENT, Mathf.Abs(movement.y));
            }
        }

        public void Moved(float time)
        {
            if (time > _soundTimes * _soundInterval)
            {
                _source.pitch = _startPitch + _pitchIncrement * _soundTimes;
                _source.Play();

                _soundTimes++;
            }
        }

        public void MovingEnded()
        {
            _soundTimes = 0;

            _animator.SetFloat(HORIZONTAL_MOVEMENT, 0f);
            _animator.SetFloat(VERTICAL_MOVEMENT, 0f);
        }
    }
}
