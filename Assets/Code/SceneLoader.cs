using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private float _fadeAnimationDuration;

        private Animator _animator;

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Restart()
        {
            StartCoroutine(Load(SceneManager.GetActiveScene().name));
        }

        private IEnumerator Load(string sceneName)
        {
            _animator.SetTrigger("FadeIn");
            yield return new WaitForSeconds(_fadeAnimationDuration);
            SceneManager.LoadScene(sceneName);
        }
    }
}
