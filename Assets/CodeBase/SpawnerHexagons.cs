using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase
{
    public class SpawnerHexagons : MonoBehaviour
    {
        [HideInInspector] public int indexHex = -1;
        [HideInInspector] public bool isCollected;
        [HideInInspector] public List<Transform> hex;

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
    }
}