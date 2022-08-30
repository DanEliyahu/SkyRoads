using System.Collections.Generic;
using UnityEngine;

namespace JellyButton
{
    public class RoadSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _roadPrefab;
        [SerializeField] private int _size = 15;
        [SerializeField] private float _offset = 10f;
        private List<Transform> _roadPieces;

        private void Awake()
        {
            _roadPieces = new List<Transform>();
            for (int i = 0; i < _size; i++)
            {
                var road = Instantiate(_roadPrefab, new Vector3(0, 0, _offset * i), Quaternion.identity).transform;
                road.SetParent(transform,false);
                _roadPieces.Add(road);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MoveRoadToBack();
            }
        }

        private void MoveRoadToBack()
        {
            var roadToMove = _roadPieces[0];
            _roadPieces.Remove(roadToMove);

            var newZPosition = _roadPieces[^1].position.z + _offset;
            var position = roadToMove.position;
            position = new Vector3(position.x, position.y, newZPosition);
            roadToMove.position = position;

            _roadPieces.Add(roadToMove);
        }
    }
}