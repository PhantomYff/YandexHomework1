using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Game
{
    public class PlayerScore : MonoBehaviour
    {
        private int _score;
        private int _playerMaxPosition;

        private const string MAX_SCORE = nameof(MAX_SCORE);
        public int MaxScore => PlayerPrefs.GetInt(MAX_SCORE, defaultValue: 0);

        public int Score
        {
            get => _score;
            private set
            {
                if (value > MaxScore)
                    PlayerPrefs.SetInt(MAX_SCORE, value);

                _score = value;
            }
        }

        public void AddScore(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Cannot add negative score.");

            Score += amount;
        }

        public void OnPlayerMovement()
        {
            int position = Mathf.RoundToInt(transform.position.x);

            if (position > _playerMaxPosition)
            {
                Score += position - _playerMaxPosition;
                _playerMaxPosition = position;
            }
        }

        [ContextMenu("Reset Max Score")]
        private void ResetMaxScore()
        {
            PlayerPrefs.SetInt(MAX_SCORE, 0);
        }
    }
}
