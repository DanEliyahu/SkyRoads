using UnityEngine;

namespace JellyButton
{
    public class ObstacleDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
