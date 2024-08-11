using Animancer;
using Animancer.Components;
using UnityEngine;

namespace Views.Modules.Impl.Animations
{
    public class ShootAnimationModule : AModule
    {
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private MotionClipTransition _shootAnimation;
        
        public void PlayShootAnimation()
        {
            _animancer.Play(_shootAnimation);
        }
        
        public void StopShootAnimation()
        {
            _animancer.Stop(_shootAnimation);
        }
    }
}