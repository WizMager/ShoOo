using System;
using System.Collections.Generic;
using Views.Impl.Ai;
using Views.Impl.Ai.Impl;

namespace Services.EnemySpawnService.Impl
{
    public class EnemySpawnService : IEnemySpawnService
    {
        private readonly List<MeleeAiView> _meleeAi = new ();
        
        
        public T Spawn<T>(EAiType aiType) where T : AAiView
        {
            switch (aiType)
            {
                case EAiType.Melee:
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(aiType), aiType, null);
            }

            return null;
        }

        public void Despawn<T>(T ai) where T : AAiView
        {
            
        }
    }
}