using TMPro;
using UnityEngine;

namespace JellyButton
{
    public class GameOverCanvasView : MonoBehaviour
    {
        [SerializeField] private string _valueTextPlaceholder = "{value}";
        [SerializeField] private TextMeshProUGUI _playingTimeText;
        [SerializeField] private TextMeshProUGUI _asteroidsPassedText;
        [SerializeField] private TextMeshProUGUI _finalScoreText;
        [SerializeField] private TextMeshProUGUI _brokenHighScoreText;

        public void SetTexts(int playingTime, int asteroidsPassed, int finalScore)
        {
            _playingTimeText.SetText(_playingTimeText.text.Replace(_valueTextPlaceholder, playingTime.ToString()));
            _asteroidsPassedText.SetText(_asteroidsPassedText.text.Replace(_valueTextPlaceholder, asteroidsPassed.ToString()));
            _finalScoreText.SetText(_finalScoreText.text.Replace(_valueTextPlaceholder, finalScore.ToString()));
        }

        public void SetBrokenHighScoreTextActive()
        {
            _brokenHighScoreText.gameObject.SetActive(true);
        }
    }
}