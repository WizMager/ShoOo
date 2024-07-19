using System.Collections.Generic;
using UnityEngine;

namespace Utils.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemySpawnDataVo> enemySpawnDataVoList;
        [SerializeField] private float spawnRadius = 5f;
        [SerializeField] private float spawnCooldown = 2f;
        [HideInInspector] public float timeAfterSpawn;

        public List<EnemySpawnDataVo> EnemySpawnDataVoList => enemySpawnDataVoList;
        public float SpawnRadius => spawnRadius;
        public float SpawnCooldown => spawnCooldown;
    }
}