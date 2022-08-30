using System.Collections;
using UnityEngine;

namespace JellyButton
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private float _distance = 10.0f;
        [SerializeField] private float _height = 5.0f;
        [SerializeField] private float _distanceDamping = 1f;
        [SerializeField] private float _heightDamping = 2.0f;
        [SerializeField] private float _rotationDamping = 3.0f;
        [SerializeField] private Transform _target;

        private Coroutine _distanceSetCoroutine;
        private void LateUpdate()
        {
            // Early out if we don't have a target
            if (!_target)
            {
                return;
            }

            Follow();
        }

        private void Follow()
        {
            var currentPosition = transform.position;
            var targetPosition = _target.position;

            // Calculate the current rotation angles
            var wantedRotationAngle = _target.eulerAngles.y;
            var wantedHeight = targetPosition.y + _height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = currentPosition.y;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle,
                _rotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            currentPosition = targetPosition - currentRotation * Vector3.forward * _distance;
            currentPosition.y = currentHeight;
            transform.position = currentPosition;

            // Always look at the target
            transform.LookAt(_target);
        }

        public void SetHeight(float newHeight)
        {
            _height = newHeight;
        }

        public void SetDistanceSmoothly(float newDistance)
        {
            if (_distanceSetCoroutine != null)
            {
                StopCoroutine(_distanceSetCoroutine);
            }
            _distanceSetCoroutine = StartCoroutine(SetDistance(newDistance));
        }

        private IEnumerator SetDistance(float targetDistance)
        {
            var startDistance = _distance;
            float percentage = 0;
            while (!Mathf.Approximately(_distance, targetDistance))
            {
                percentage += _distanceDamping * Time.deltaTime;
                _distance = Mathf.Lerp(startDistance, targetDistance, percentage);
                yield return null;
            }
        }
    }
}