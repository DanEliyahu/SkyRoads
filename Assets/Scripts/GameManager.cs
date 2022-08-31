using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace JellyButton
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ObjectPoolsData _objectPoolsData;
        [SerializeField] private int _scorePerSecond = 1;
        [SerializeField] private int _scorePerSecondWhenBoosting = 2;
        public UnityEvent _onGameStarted;

        public ObjectPooler ObjectPooler { get; private set; }

        private int _score;
        private int _currentScorePerSecond;
        private int _passedObstacles;
        private int _playingTimeInSeconds;
        private bool _hasGameStarted;
        private bool _isGameOver;
        private float _startTime;

        private const string HIGH_SCORE_KEY = "highscore";

        private void Awake()
        {
            ObjectPooler = new ObjectPooler(_objectPoolsData);
            _currentScorePerSecond = _scorePerSecond;
        }

        private void Update()
        {
            if (_hasGameStarted) return;
            if (Input.anyKeyDown)
            {
                StartGame();
                _hasGameStarted = true;
            }
        }

        private void StartGame()
        {
            StartCoroutine(IncrementScorePerSecond());
            _onGameStarted?.Invoke();
            _startTime = Time.time;
        }

        private IEnumerator IncrementScorePerSecond()
        {
            while (!_isGameOver)
            {
                yield return new WaitForSeconds(1f);
                _score += _currentScorePerSecond;
            }
        }

        public void SetBoostScoreMode(bool value)
        {
            _currentScorePerSecond = value ? _scorePerSecondWhenBoosting : _scorePerSecond;
        }

        public void AddToScore(int amount)
        {
            _score += amount;
        }

        public void GameOver()
        {
            _isGameOver = true;
            SetHighScore();
            var timePlayedInSeconds = Mathf.FloorToInt(Time.time - _startTime);
        }

        private void SetHighScore()
        {
            var highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
            if (highScore < _score)
            {
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, _score);
            }
        }
    }
}