using CodeBase.Helpers;
using UnityEngine;

namespace CodeBase.Hexagons
{
    public class Bucket : MonoBehaviour
    {
        private const string Ball = "Ball";
        
        [SerializeField] private SpawnerHexagons spawnerHexagons;

        private bool _isCollected;
        private static FXHolder FXHolder => FXHolder.Instance;
        
        private  void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Ball) && !spawnerHexagons.isCollectedBucket && !_isCollected)
            {
                _isCollected = true;
                spawnerHexagons.indexHex++;
                spawnerHexagons.isCollectedBucket = true;
                FXHolder.SpawnBucketFx(other.gameObject.transform);
                spawnerHexagons.bucketCompleted.Invoke();
            }
        }

        private void IncrementIndex()
        {
            spawnerHexagons.isCollectedBucket = true;
            spawnerHexagons.indexHex++;
        }
        
        
    }
}