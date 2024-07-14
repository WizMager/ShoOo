using UniRx;
using UnityEngine;

namespace Views.Modules.Impl
{
    public class MoveModule : AModule
    {
        [SerializeField] private float moveSpeed = 5f;

        private Transform _rootTransform;

        public override void Initialize(AView view, CompositeDisposable disposable)
        {
            _rootTransform = view.transform;
            
            base.Initialize(view, disposable);
        }

        public void MoveCharacter(Vector3 direction)
        {
            _rootTransform.position += direction * Time.deltaTime * moveSpeed;
        }
    }
}