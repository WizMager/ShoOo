using Views.Impl.Ai;

namespace Services.EnemySpawnService
{
    public interface IEnemySpawnService
    {
        T Spawn<T>(EAiType aiType) where T : AAiView;
        
        void Despawn<T>(T ai) where T : AAiView;
    }
}