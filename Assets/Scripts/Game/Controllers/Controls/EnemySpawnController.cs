using System.Collections.Generic;
using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using Services.EnemySpawnService;
using UnityEngine;
using Utils.EnemySpawner;

namespace Game.Controllers.Controls
{
    [Install(EExecutionPriority.Normal, 100)]
    public class EnemySpawnController : IFixedUpdatable
    {
        private readonly List<EnemySpawner> _enemySpawners;
        private readonly IEnemySpawnService _enemySpawnService;

        public EnemySpawnController(
            IGameFieldProvider gameFieldProvider, 
            IEnemySpawnService enemySpawnService
        )
        {
            _enemySpawnService = enemySpawnService;
            _enemySpawners = gameFieldProvider.GameField.EnemySpawners;
        }
        
        public void FixedUpdate()
        {
            if (_enemySpawners.Count == 0) 
                return;
            
            CheckNeedSpawn();
        }
        
        private void CheckNeedSpawn()
        {
            foreach (var enemySpawner in _enemySpawners)
            {
                if (enemySpawner.SpawnCooldown > enemySpawner.timeAfterSpawn)
                {
                    enemySpawner.timeAfterSpawn += Time.fixedDeltaTime;
                    
                    continue;
                }

                enemySpawner.timeAfterSpawn = 0;
                
                foreach (var spawnDataVo in enemySpawner.EnemySpawnDataVoList)
                {
                    if (spawnDataVo.currentNumber >= spawnDataVo.maxNumber) 
                        continue;
                    
                    spawnDataVo.currentNumber++;
                    var isSpawned = TrySpawnEnemy(spawnDataVo.aiType, enemySpawner.transform.position, enemySpawner.SpawnRadius);
                    
                    if (!isSpawned)
                    {
                        enemySpawner.timeAfterSpawn = enemySpawner.SpawnCooldown;
                    }
                }
            }
        }

        private bool TrySpawnEnemy(EAiType aiType, Vector3 spawnerPosition, float spawnRadius)
        {
            var aiView = _enemySpawnService.Spawn(aiType);
            
            if (!aiView)
            {
                return false;
            }
            
            var randomPosition = Random.insideUnitCircle * spawnRadius;
            var position = spawnerPosition + new Vector3(randomPosition.x, 0, randomPosition.y);
            aiView.transform.position = position;
            aiView.ActivateAi();

            return true;
        }
    }
}