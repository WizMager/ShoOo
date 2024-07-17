using Views.Impl.Ai;

namespace Services.EnemySpawnService
{
    public interface IEnemySpawnService
    {
        AAiView Spawn(EAiType aiType);
        
        void Despawn(AAiView ai);
    }
}