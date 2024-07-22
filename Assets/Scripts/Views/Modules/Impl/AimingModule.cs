using R3;
using UnityEngine;

namespace Views.Modules.Impl
{
    public class AimingModule : AModule
    {
        private Transform _rootTransform;

        public override void Initialize(AView view, CompositeDisposable disposable)
        {
            _rootTransform = view.transform;
            
            base.Initialize(view, disposable);
        }

        public void RotateCharacter(Vector3 pointerPosition)
        {
            var lookPointerDirection = pointerPosition - _rootTransform.position;
            var angle = Mathf.Atan2(lookPointerDirection.x, lookPointerDirection.z) * Mathf.Rad2Deg;
            
            _rootTransform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}