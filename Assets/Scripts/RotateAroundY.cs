using UnityEngine;

namespace JellyButton
{
    public class RotateAroundY : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 360f;

        private void Update()
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
        }
    }
}