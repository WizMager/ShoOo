using R3;
using Views.Impl.Ai;

namespace Services.EnemySpawnService
{
    public interface IEnemySpawnService
    {
        Observable<AAiView[]> AiInstantiated { get; }
        
        (bool isNewAi, AAiView aiView) Spawn(EAiType aiType);
        void Despawn(AAiView ai);
    }
}