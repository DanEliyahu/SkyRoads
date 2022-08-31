using UnityEngine;

namespace JellyButton
{
    public class ObstacleDestroyer : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                other.gameObject.SetActive(false);
                _gameManager.AddToScore(other.GetComponent<Obstacle>().ScoreWhenPassed);
                _gameManager.IncrementNumberOfAsteroidsPassed();
            }
        }
    }
}