using System.Collections.Generic;
using UnityEngine;

namespace JellyButton
{
    public class ObjectPooler
    {
        private Dictionary<ObjectType, Queue<GameObject>> _poolDictionary;

        public ObjectPooler(ObjectPoolsData data)
        {
            _poolDictionary = new Dictionary<ObjectType, Queue<GameObject>>();

            foreach (var pool in data._pools)
            {
                GameObject poolParent = new GameObject(pool._tag.ToString());
                var objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool._size; i++)
                {
                    var obj = Object.Instantiate(pool._prefab, poolParent.transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                _poolDictionary.Add(pool._tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(ObjectType tag, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("There is no pool with tag " + tag);
                return null;
            }

            GameObject objectToSpawn = _poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            _poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}