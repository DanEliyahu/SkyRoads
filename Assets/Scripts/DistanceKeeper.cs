using UnityEngine;

namespace JellyButton
{
    public class DistanceKeeper : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private Vector3 _offset;
    
        // Start is called before the first frame update
        void Start()
        {
            _offset = transform.position - _target.position;
        }
        
        void LateUpdate()
        {
            transform.position = _target.position + _offset;
        }
    }
}
