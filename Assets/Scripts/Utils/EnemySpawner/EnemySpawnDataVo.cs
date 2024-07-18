using System;
using Services.EnemySpawnService;
using UnityEngine;

namespace Utils.EnemySpawner
{
    [Serializable]
    public class EnemySpawnDataVo
    {
        public EAiType aiType;
        public int maxNumber;
        [HideInInspector] public int currentNumber;
    }
}