using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace JellyButton
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ObjectPoolsData _objectPoolsData;
        [SerializeField] private int _scorePerSecond = 1;
        [SerializeField] private int _scorePerSecondWhenBoosting = 2;
        [SerializeField] private GameplayCanvasView _gameplayCanvasView;
        [SerializeField] private GameOverCanvasView _gameOverCanvasView;
        public UnityEvent _onGameStarted;
        public UnityEvent _onGameOver;

        public ObjectPooler ObjectPooler { get; private set; }

        private int _score;
        private int _highScore;
        private int _currentScorePerSecond;
        private int _passedObstacles;
        private int _playingTimeInSeconds;
        private bool _hasGameStarted;
        private bool _isGameOver;

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
            _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
            InitializeUI();
            StartCoroutine(IncrementScorePerSecond());
            _onGameStarted?.Invoke();
        }

        private void InitializeUI()
        {
            _gameplayCanvasView.SetTimeText(_playingTimeInSeconds);
            _gameplayCanvasView.SetAsteroidsPassedText(_passedObstacles);
            _gameplayCanvasView.SetScoreText(_score);
            _gameplayCanvasView.SetHighScoreText(_highScore);
        }

        private IEnumerator IncrementScorePerSecond()
        {
            while (!_isGameOver)
            {
                yield return new WaitForSeconds(1f);

                _score += _currentScorePerSecond;
                UpdateScoreTexts();

                _playingTimeInSeconds++;
                _gameplayCanvasView.SetTimeText(_playingTimeInSeconds);
            }
        }

        private void UpdateScoreTexts()
        {
            _gameplayCanvasView.SetScoreText(_score);
            _gameplayCanvasView.SetHighScoreText(Mathf.Max(_score, _highScore));
        }

        public void SetBoostScoreMode(bool value)
        {
            _currentScorePerSecond = value ? _scorePerSecondWhenBoosting : _scorePerSecond;
        }

        public void AddToScore(int amount)
        {
            _score += amount;
            UpdateScoreTexts();
        }

        public void IncrementNumberOfAsteroidsPassed()
        {
            _passedObstacles++;
            _gameplayCanvasView.SetAsteroidsPassedText(_passedObstacles);
        }

        public void GameOver()
        {
            _isGameOver = true;
            SetHighScore();
            InitializeGameOverUI();
            _onGameOver?.Invoke();
        }

        private void SetHighScore()
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, Mathf.Max(_score, _highScore));
        }

        private void InitializeGameOverUI()
        {
            _gameOverCanvasView.SetTexts(_playingTimeInSeconds, _passedObstacles, _score);
            if (_score > _highScore)
            {
                _gameOverCanvasView.SetBrokenHighScoreTextActive();
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}