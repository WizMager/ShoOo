using Services.EnemySpawnService;
using UnityEngine.AddressableAssets;

namespace Configs.PrefabBase
{
    public interface IPrefabBase
    {
        AssetReference GetAiPrefabWithType(EAiType aiType);
    }
}