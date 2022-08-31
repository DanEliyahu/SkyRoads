using UnityEngine;

namespace JellyButton
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private int _scoreWhenPassed = 5;

        public int ScoreWhenPassed => _scoreWhenPassed;

        private void OnTriggerEnter(Collider other)
        {
            var destroyable = other.GetComponent<IDestroyable>();
            destroyable?.Destroy();
        }
    }
}