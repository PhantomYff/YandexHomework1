using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(SpriteRenderer), typeof(AudioSource), typeof(Collider2D))]
    public class GameButton : MonoBehaviour
    {
        [SerializeField] private Sprite _onPressedSprite;
        [SerializeField] private UnityEvent _pressed;

        private bool _beenPressed;

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<PlayerMovement>(out _))
            {
                if (_beenPressed)
                    return;

                GetComponent<SpriteRenderer>().sprite = _onPressedSprite;
                GetComponent<AudioSource>().Play();
                _pressed.Invoke();

                _beenPressed = true;
            }
        }
    }
}
