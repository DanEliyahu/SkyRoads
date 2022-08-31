using UnityEngine;
using TMPro;

namespace JellyButton
{
    public class GameplayCanvasView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _asteroidsPassedText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        public void SetTimeText(int time)
        {
            _timeText.SetText(time.ToString());
        }
        
        public void SetAsteroidsPassedText(int amount)
        {
            _asteroidsPassedText.SetText(amount.ToString());
        }
        
        public void SetScoreText(int score)
        {
            _scoreText.SetText(score.ToString());
        }
        
        public void SetHighScoreText(int score)
        {
            _highScoreText.SetText(score.ToString());
        }
    }
}