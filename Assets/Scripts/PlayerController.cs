using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace JellyButton
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour, IDestroyable
    {
        [Header("Movement")] 
        [SerializeField] private float _forwardSpeed;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _horizontalMovementBoundary = 5f;

        [Header("Rotation")] 
        [SerializeField] private float _maxHorizontalTilt = 45f;
        [SerializeField] private float _tiltSpeed = 60f;

        [Header("Boost")] 
        [SerializeField] private float _boostMultiplier = 2f;
        [SerializeField] private float _boostDuration = 1f;
        public UnityEvent _onBoostStart;
        public UnityEvent _onBoostEnd;


        private Rigidbody _rigidbody;
        private float _horizontalInput;
        private bool _canBoost = true;
        private bool _startBoost;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = Vector3.forward * _forwardSpeed;
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            if (_canBoost && Input.GetButtonDown("Jump"))
            {
                _startBoost = true;
            }
        }

        private void FixedUpdate()
        {
            if (_startBoost)
            {
                StartCoroutine(StartBoost());
                _startBoost = false;
            }
            var horizontalMovement = _horizontalInput * _horizontalSpeed * Time.fixedDeltaTime;

            MovePlayer(horizontalMovement);
            TiltPlayer(_horizontalInput);
        }

        private void MovePlayer(float horizontalMovement)
        {
            var position = transform.position;
            var newXPosition = Mathf.Clamp(position.x + horizontalMovement, -_horizontalMovementBoundary,
                _horizontalMovementBoundary);
            _rigidbody.MovePosition(new Vector3(newXPosition, position.y, position.z));
        }

        private void TiltPlayer(float horizontalInput)
        {
            var targetRotation = Quaternion.Euler(0, 0, _maxHorizontalTilt * -horizontalInput);
            var actualRotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, _tiltSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(actualRotation);
        }

        private IEnumerator StartBoost()
        {
            _rigidbody.velocity = _forwardSpeed * _boostMultiplier * Vector3.forward;
            _canBoost = false;
            _onBoostStart?.Invoke();

            yield return new WaitForSeconds(_boostDuration);
            
            // ReSharper disable once Unity.InefficientPropertyAccess - need to modify directly
            _rigidbody.velocity = _forwardSpeed * Vector3.forward;
            _canBoost = true;
            _onBoostEnd?.Invoke();
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}