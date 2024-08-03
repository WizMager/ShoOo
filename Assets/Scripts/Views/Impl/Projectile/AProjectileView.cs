using R3;
using UnityEngine;

namespace Views.Impl.Projectile
{
    public class AProjectileView : AView
    {
        protected float Damage;
        protected readonly ReactiveCommand<Unit> ExistProjectileEndedCommand = new ();
        
        [SerializeField] private float timeExist = 5f;
        [SerializeField] private Renderer trailRenderer;

        private readonly CompositeDisposable _disposable = new ();
        private float _leftTimeExist;
        
        public Observable<Unit> ExistProjectileEnded => ExistProjectileEndedCommand;
        
        public virtual void ResetProjectile()
        {
            trailRenderer.enabled = false;
            
            _disposable?.Clear();
            
            gameObject.SetActive(false);
        }

        public void ActivateProjectile(float projectileDamage)
        {
            trailRenderer.enabled = true;
            
            Damage = projectileDamage;
            
            gameObject.SetActive(true);
            
            _leftTimeExist = timeExist;
            
            Observable.EveryUpdate().Subscribe(_ => OnTimerUpdating(Time.deltaTime)).AddTo(_disposable);
        }

        protected virtual void OnTimerUpdating(float deltaTime)
        {
            if (_leftTimeExist > 0)
            {
                _leftTimeExist -= deltaTime;
            }
            else
            {
                _disposable.Clear();
                
                ExistProjectileEndedCommand.Execute(default);
            }
        }
    }
}