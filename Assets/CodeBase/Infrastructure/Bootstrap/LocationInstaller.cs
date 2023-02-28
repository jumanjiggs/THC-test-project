using CodeBase.AI;
using CodeBase.Character;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform characterSpawnPoint;
        public Transform enemySpawnPoint;
        public GameObject characterPrefab;
        public GameObject enemyPrefab;
        public SpawnerHexagons spawnerHexagons;

        public override void InstallBindings()
        {
            BindPlayer();
            BindEnemy();
        }

        private void BindPlayer()
        {
            Player player = Container.InstantiatePrefabForComponent<Player>(characterPrefab, characterSpawnPoint.position,
                Quaternion.identity, null);
            Container.Bind<Player>().FromInstance(player).AsSingle();
            player.spawnerHexagons = spawnerHexagons;
        }

        private void BindEnemy()
        {
            Enemy enemy = Container.InstantiatePrefabForComponent<Enemy>(enemyPrefab, enemySpawnPoint.position,
                Quaternion.identity, null);
            Container.Bind<Enemy>().FromInstance(enemy).AsSingle();
            // enemy.spawnerHexagons = spawnerHexagons;
        }
        
    }
}