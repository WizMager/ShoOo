using System;
using Configs.PrefabBase;
using Utils.ObjectPool;
using Views.Impl.Ai;
using Views.Impl.Ai.Impl;
using Zenject;

namespace Services.EnemySpawnService.Impl
{
    public class EnemySpawnService : IEnemySpawnService, IInitializable
    {
        private readonly IPrefabBase _prefabBase;
        private AiPool<MeleeAiView> _meleeAiPool;

        public EnemySpawnService(IPrefabBase prefabBase)
        {
            _prefabBase = prefabBase;
        }

        public void Initialize()
        {
            var enemyTypes = Enum.GetValues(typeof(EAiType));
            for (var i = 0; i < enemyTypes.Length; i++)
            {
                var type = enemyTypes.GetValue(i);
                switch ((EAiType) type)
                {
                    case EAiType.Melee:
                        var prefab = _prefabBase.GetAiPrefabWithType(EAiType.Melee);
                        _meleeAiPool = new AiPool<MeleeAiView>(prefab);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        public AAiView Spawn(EAiType aiType)
        {
            switch (aiType)
            {
                case EAiType.Melee:
                    return _meleeAiPool.GetAi();
                default:
                    throw new ArgumentOutOfRangeException(nameof(aiType), aiType, null);
            }
        }

        public void Despawn(AAiView ai)
        {
            switch (ai.AiType)
            {
                case EAiType.Melee:
                    _meleeAiPool.ReleaseAi(ai as MeleeAiView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}