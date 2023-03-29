using UnityEngine;
using YX.KeyboardBinding;
using TMPro;

namespace Game
{
    public class GameFinisher : MonoBehaviour
    {
        [SerializeField, TextArea] private string _deathMessageText;

        [SerializeField] private TMP_Text _deathMessageDisplay;
        [SerializeField] private KeyCode _restartKey = KeyCode.Return; // Return - это Enter.
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private PlayerScore _score;

        protected void Start()
        {
            Keyboard.BindTo(_restartKey).Pressed(_sceneLoader.Restart);

            _deathMessageDisplay.text =
                string.Format(_deathMessageText, _score.Score, _score.MaxScore);
        }
    }
}
