using System;
using Views.Impl.Ai;

namespace Services.EnemySpawnService
{
    public interface IEnemySpawnService
    {
        Action<AAiView[]> AiInstantiated { get; set; }
        
        (bool isNewAi, AAiView aiView) Spawn(EAiType aiType);
        void Despawn(AAiView ai);
    }
}