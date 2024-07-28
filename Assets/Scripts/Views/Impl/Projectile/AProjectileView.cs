using R3;
using UnityEngine;

namespace Views.Impl.Projectile
{
    public class AProjectileView : AView
    {
        protected float Damage;
        protected readonly ReactiveCommand<Unit> ExistProjectileEndedCommand = new ();
        
        [SerializeField] private float timeExist = 5f;
        
        private readonly CompositeDisposable _disposable = new ();
        private float _leftTimeExist;
        
        public Observable<Unit> ExistProjectileEnded => ExistProjectileEndedCommand;
        
        public virtual void ResetProjectile()
        {
            _disposable.Dispose();
            
            gameObject.SetActive(false);
        }

        public void ActivateProjectile(float projectileDamage)
        {
            Damage = projectileDamage;
            
            gameObject.SetActive(true);
            
            _leftTimeExist = timeExist;
            
            Observable.EveryUpdate(UnityFrameProvider.TimeUpdate).Subscribe(_ => OnTimerUpdating(Time.deltaTime)).AddTo(_disposable);//TODO: dont work idk why
        }

        protected virtual void OnTimerUpdating(float deltaTime)
        {
            if (_leftTimeExist > 0)
            {
                _leftTimeExist -= deltaTime;
            }
            else
            {
                _disposable.Dispose();
                
                ExistProjectileEndedCommand.Execute(default);
            }
        }
    }
}