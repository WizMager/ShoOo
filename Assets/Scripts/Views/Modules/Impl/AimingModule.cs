using UniRx;
using Unity.Mathematics;
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
            var pos = new Vector3(pointerPosition.x, 0, pointerPosition.y);
            var curPos = _rootTransform.position;
            curPos.y = 0;
            var lookDirection = pos - curPos;
            //var aimDirection = (pointerPosition - _rootTransform.position).normalized;
            //var rotation = Quaternion.LookRotation(lookDirection);
            //_rootTransform.rotation = rotation;
            var angle = math.atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
            //_rootTransform.rotation = Quaternion.Euler(0, angle, 0);
            Debug.Log($"{pointerPosition}/{lookDirection}/{angle}");
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }
}