using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Hexagons
{
    public class SpawnerHexagons : MonoBehaviour
    {
        public UnityEvent bucketCompleted;
        public UnityEvent allBucketsCompleted;
        public int indexHex; 
        
        [HideInInspector]public bool isCollectedBucket;
        [HideInInspector] public List<Transform> hex;

        [SerializeField] private List<AssetReference> hexagons;
        [SerializeField] private Transform hexagonHolder;
        [SerializeField] private float offsetZ;

        private Vector3 _firstHexPosition;
        private AsyncOperationHandle<GameObject> _currentObj;


        private void Start()
        {
            SpawnHexagons();
        }

        private void SpawnHexagons()
        {
            _firstHexPosition = hexagonHolder.position;
            foreach (var handle in hexagons.Select(hexagon =>
                         Addressables.InstantiateAsync(hexagon, _firstHexPosition, Quaternion.identity, hexagonHolder)))
            {
                _firstHexPosition.z += offsetZ;
                handle.Completed += HandleOnCompleted;
            }
        }

        private void HandleOnCompleted(AsyncOperationHandle<GameObject> obj)
        {
            hex.Add(obj.Result.gameObject.transform);
        }

        public Vector3 GetNextHexagonPosition()
        {
            return new Vector3(hex[indexHex].transform.position.x, hex[indexHex].transform.position.y + 2f,
                hex[indexHex].transform.position.z);
        }

        public void CheckAllHexagonPassed()
        {
            if (indexHex == hex.Count - 2)
                allBucketsCompleted.Invoke();
        }
    }
}