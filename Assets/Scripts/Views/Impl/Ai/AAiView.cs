using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Views.Impl.Ai
{
    public abstract class AAiView : AView, IAi
    {
        [SerializeField] private BehaviorTree behaviorTree;
        
        public BehaviorTree BehaviorTree => behaviorTree;
    }
}