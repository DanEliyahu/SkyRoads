using UnityEngine;

namespace JellyButton
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ObjectPoolsData _objectPoolsData;

        public ObjectPooler ObjectPooler { get; private set; }

        private void Awake()
        {
            ObjectPooler = new ObjectPooler(_objectPoolsData);
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}