using System;
using Services.EnemySpawnService;
using UnityEngine;

namespace Configs.PrefabBase
{
    [Serializable]
    public class AiPrefabVo
    {
        public EAiType aiType;
        public GameObject prefab;
    }
}