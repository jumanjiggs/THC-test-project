using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Character
{
    public class CharacterSpawn : MonoBehaviour
    {
        public AssetReference characterPrefab;

        private void Start()
        {
            SpawnCharacter();
        }

        private void SpawnCharacter()
        {
            Addressables.InstantiateAsync(characterPrefab, new Vector3(0, 32, 0), Quaternion.identity, transform);
        }
    }
}
