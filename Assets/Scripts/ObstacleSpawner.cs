using System.Collections;
using UnityEngine;

namespace JellyButton
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectType _obstacleType;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _horizontalBoundary;
        [SerializeField] private float _startSpawnDelay = 2f;
        [SerializeField] private float _endSpawnDelay = 0.5f;
        [SerializeField] private float _timeToReachEndDelay = 20f;

        private ObjectPooler _objectPooler;
        private bool _shouldSpawn;
        private float _spawnDelay;
        private float _spawnDelayPercentage;

        private void Start()
        {
            _objectPooler = FindObjectOfType<GameManager>().ObjectPooler;
            _spawnDelay = _startSpawnDelay;
        }

        private void Update()
        {
            if (_shouldSpawn && !Mathf.Approximately(_spawnDelay, _endSpawnDelay))
            {
                _spawnDelayPercentage += Time.deltaTime / _timeToReachEndDelay;
                _spawnDelay = Mathf.Lerp(_startSpawnDelay, _endSpawnDelay, _spawnDelayPercentage);
            }
        }

        private IEnumerator SpawnObstacles()
        {
            while (_shouldSpawn)
            {
                var spawnPosition = _spawnPoint.position;
                spawnPosition.x = Random.Range(-_horizontalBoundary, _horizontalBoundary);
                _objectPooler.SpawnFromPool(_obstacleType, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        public void StartSpawning()
        {
            _shouldSpawn = true;
            StartCoroutine(SpawnObstacles());
        }

        public void StopSpawning()
        {
            _shouldSpawn = false;
        }
    }
}