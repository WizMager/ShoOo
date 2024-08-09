using Views.Impl.Ai;

namespace Services.EnemySpawnService
{
    public interface IEnemySpawnService
    {
        (bool isNewAi, AAiView aiView) Spawn(EAiType aiType);
        
        void Despawn(AAiView ai);
    }
}