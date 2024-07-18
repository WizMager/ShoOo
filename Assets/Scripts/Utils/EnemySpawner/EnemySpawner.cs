using System.Collections.Generic;
using UnityEngine;

namespace Utils.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemySpawnDataVo> enemySpawnDataVoList;

        public List<EnemySpawnDataVo> EnemySpawnDataVoList => enemySpawnDataVoList;
    }
}