using CodeBase.Character;
using CodeBase.Enemies;
using CodeBase.Hexagons;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class LocationInstaller : MonoInstaller
    {
        [Header("PLAYER")]
        public Transform characterSpawnPoint;
        public GameObject characterPrefab;
        public SpawnerHexagons spawnerHexagonsPlayer;
        [Header("ENEMY")]
        public GameObject enemyPrefab;
        public Transform enemySpawnPoint;
        public SpawnerHexagons spawnerHexagonsEnemy;

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
            player.Initialize(spawnerHexagonsPlayer);
            player.GetComponent<PlayerAnimator>().Initialize(spawnerHexagonsPlayer);
        }

        private void BindEnemy()
        {
            Enemy enemy = Container.InstantiatePrefabForComponent<Enemy>(enemyPrefab, enemySpawnPoint.position,
                Quaternion.identity, null);
            Container.Bind<Enemy>().FromInstance(enemy).AsSingle();
             enemy.Initialize(spawnerHexagonsEnemy);
        }
        
    }
}