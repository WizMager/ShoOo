using Services.EnemySpawnService;
using UnityEngine;

namespace Configs.PrefabBase
{
    public interface IPrefabBase
    {
        GameObject GetAiPrefabWithType(EAiType aiType);
    }
}