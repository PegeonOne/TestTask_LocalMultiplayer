using Game.App.Utils;
using Game.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Game.App.Enemy
{
    public sealed class EnemiesManager : BaseController
    {
        [SerializeField] private GameObject EnemyPrefab;
        private VectorRandomiser _vectorRandomiser;
        private int _maxEnemyCount;
        private List<GameObject> _enemies;

        public override void Init(AppConfig config)
        {
            _enemies = new List<GameObject>();
            _vectorRandomiser = new VectorRandomiser(config.SpawnBoxArea.x, config.SpawnBoxArea.y);
            _maxEnemyCount = config.MaxEnemyCount;           
        }

        public override void OnNetworkSpawn()
        {
            if (IsHost)
            {
                //not optimised
                StartCoroutine(SpawnEnemy());
            }
        }

        private void CreateEnemyObject()
        {          
            if (_enemies.Count < _maxEnemyCount)
            {
                GameObject enemy = Instantiate(EnemyPrefab);
                enemy.transform.position = _vectorRandomiser.GetRandomSpawnPosition();
                NetworkObject networkObject = enemy.GetComponent<NetworkObject>();
                networkObject.Spawn(true);
                _enemies.Add(enemy);
            }
        }

        private IEnumerator SpawnEnemy()
        {
            CreateEnemyObject();
            yield return new WaitForSeconds(5);
            StartCoroutine(SpawnEnemy());
        }
    }
}
