using System;
using UnityEngine;
using TMPro;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    public class BoostDisplay : MonoBehaviour, IBoostsHandler
    {
        [SerializeField] private TMP_Text _display;
        [SerializeField] private DisplaySettings _speedUpBoost;
        [SerializeField] private DisplaySettings _noClipBoost;

        private Animator _animator;

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnSpeedUp()
        {
            _speedUpBoost.ApplyToDisplay(_display, _animator);
        }

        public void OnNoClip()
        {
            _noClipBoost.ApplyToDisplay(_display, _animator);
        }

        public void OnBoostReset() { }

        [Serializable]
        private struct DisplaySettings
        {
            [SerializeField] private string text;
            [SerializeField] private Color color;

            public void ApplyToDisplay(TMP_Text display, Animator animator)
            {
                display.text = text;
                display.color = color;

                animator.SetTrigger("Show");
            }
        }
    }
}
