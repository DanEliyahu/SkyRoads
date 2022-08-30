using UnityEngine;

namespace JellyButton
{
    public class Obstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var destroyable = other.GetComponent<IDestroyable>();
            destroyable?.Destroy();
        }
    }
}
