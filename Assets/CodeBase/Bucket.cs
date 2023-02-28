using CodeBase.Helpers;
using UnityEngine;

namespace CodeBase
{
    public class Bucket : MonoBehaviour
    {
        private const string Ball = "Ball";
        
        [SerializeField] private SpawnerHexagons spawnerHexagons;

        private static EventsHolder EventsHolder => EventsHolder.Instance;
        private static FXHolder FXHolder => FXHolder.Instance;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Ball) && !spawnerHexagons.isCollected)
            {
                IncrementIndex();
                FXHolder.SpawnBucketFx(other.gameObject.transform);
                EventsHolder.bucketCompleted.Invoke();
            }
        }

        private void IncrementIndex()
        {
            spawnerHexagons.isCollected = true;
            spawnerHexagons.indexHex++;
        }
    }
}