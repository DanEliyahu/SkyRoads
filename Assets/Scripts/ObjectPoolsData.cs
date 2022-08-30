using System.Collections.Generic;
using UnityEngine;

namespace JellyButton
{
    public enum ObjectType
    {
        Asteroids
    }

    [System.Serializable]
    public struct Pool
    {
        public ObjectType _tag;
        public GameObject _prefab;
        public int _size;
    }

    [CreateAssetMenu(menuName = "ObjectPoolsData", fileName = "ObjectPoolsData")]
    public class ObjectPoolsData : ScriptableObject
    {
        public List<Pool> _pools;
    }
}