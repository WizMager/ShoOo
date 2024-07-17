using BehaviorDesigner.Runtime;
using Services.EnemySpawnService;
using UnityEngine;

namespace Views.Impl.Ai
{
    public abstract class AAiView : AView, IAi
    {
        [SerializeField] private BehaviorTree behaviorTree;
        [SerializeField] private EAiType aiType;
        
        public BehaviorTree BehaviorTree => behaviorTree;
        public EAiType AiType => aiType;

        public void ResetAi()
        {
            //TODO: realize reset state of ai
        }
    }
}