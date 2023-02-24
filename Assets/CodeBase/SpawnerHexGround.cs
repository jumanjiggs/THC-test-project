using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase
{
    public class SpawnerHexGround : MonoBehaviour
    {
        [SerializeField] private List<AssetReference> hexagons;
        [SerializeField] private Transform hexagonHolder;
        [SerializeField] private float offsetZ;

        private Vector3 _firstHexPosition;
        private AsyncOperationHandle<IList<AssetReference>> _loadHandle;

        private void Start()
        {
            SpawnHexagons();
        }
        
        private void SpawnHexagons()
        {
            foreach (var hexagon in hexagons)
            {
                var handle = Addressables.InstantiateAsync(hexagon, _firstHexPosition, Quaternion.identity, hexagonHolder);
                _firstHexPosition.z += offsetZ;
                handle.Completed += HandleOnCompleted;
            }
        }

        private void HandleOnCompleted(AsyncOperationHandle<GameObject> obj)
        {
           
        }
    }
}