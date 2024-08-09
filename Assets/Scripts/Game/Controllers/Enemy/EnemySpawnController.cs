using System.Collections.Generic;
using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using R3;
using Services.EnemySpawnService;
using UnityEngine;
using Utils.EnemySpawner;
using Views.Impl.Ai;
using Views.Modules.Impl;

namespace Game.Controllers.Enemy
{
    [Install(EExecutionPriority.Normal, 100)]
    public class EnemySpawnController : IFixedUpdatable
    {
        private readonly List<EnemySpawner> _enemySpawners;
        private readonly IEnemySpawnService _enemySpawnService;
        private readonly CompositeDisposable _disposable = new ();

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
            // if (_enemySpawners.Count == 0) 
            //     return;
            
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
            Debug.Log("TRY SPAWN");
            var spawnTulip = _enemySpawnService.Spawn(aiType);
            
            if (!spawnTulip.aiView)
            {
                return false;
            }
            
            var healthModule = spawnTulip.aiView.GetModule<HealthModule>();
            if (!healthModule)
            {
                Debug.LogError($"[{nameof(EnemySpawnController)}]: There is no {nameof(HealthModule)} on this aiView: {spawnTulip.aiView.gameObject.name}");
            }
            else
            {
                if (spawnTulip.isNewAi || !_disposable.Contains(spawnTulip.aiView))
                {
                    Debug.Log($"Check subscribe aiView: {spawnTulip.aiView.gameObject.name}/{spawnTulip.aiView.GetHashCode()}");
                    healthModule.AiExistEnded.Subscribe(_ => OnAiExistEnded(spawnTulip.aiView)).AddTo(_disposable);
                }
            }
            
            var randomPosition = Random.insideUnitCircle * spawnRadius;
            var position = spawnerPosition + new Vector3(randomPosition.x, 0, randomPosition.y);
            spawnTulip.aiView.transform.position = position;
            spawnTulip.aiView.ActivateAi();

            return true;
        }

        private void OnAiExistEnded(AAiView aiView)
        {
            _enemySpawnService.Despawn(aiView);
        }
    }
}