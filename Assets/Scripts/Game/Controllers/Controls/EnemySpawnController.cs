using System.Collections.Generic;
using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using Services.EnemySpawnService;
using Utils.EnemySpawner;

namespace Game.Controllers.Controls
{
    [Install(EExecutionPriority.Normal, 100)]
    public class EnemySpawnController : IUpdatable
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

        public void Update()
        {
            
        }

        private void CheckNeedSpawn()
        {
            foreach (var enemySpawner in _enemySpawners)
            {
                
            }
        }

        private void SpawnEnemy()
        {
            
        }
    }
}