using UnityEngine;
using UnityEngine.Events;
using YX.KeyboardBinding;

namespace Game
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private UnityEvent _started;

        private Binding _binding;

        protected void Awake()
        {
            _binding = Keyboard.BindToAnyKey(delegate
            {
                _started.Invoke();
                _binding.Unbind();
                Destroy(this);
            });
        }
    }
}
