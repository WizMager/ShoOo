using System;
using Services.EnemySpawnService;
using UnityEngine.AddressableAssets;

namespace Configs.PrefabBase
{
    [Serializable]
    public class AiPrefabVo
    {
        public EAiType aiType;
        public AssetReference prefab;
    }
}