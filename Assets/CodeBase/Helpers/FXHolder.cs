using UnityEngine;

namespace CodeBase.Helpers
{
    public class FXHolder : Singleton<FXHolder>
    {
        [SerializeField] private GameObject fxBucket;
        
        public void SpawnBucketFx(Transform spawnPosition) => 
            Instantiate(fxBucket, spawnPosition.position, fxBucket.transform.rotation);
    }
}