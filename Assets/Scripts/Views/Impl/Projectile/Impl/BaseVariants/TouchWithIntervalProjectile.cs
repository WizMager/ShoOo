using UnityEngine;
using Utils.LayerMask;

namespace Views.Impl.Projectile.Impl.BaseVariants
{
    public class TouchWithIntervalProjectile : AProjectileView
    {
        [SerializeField] private float damageTakeInterval = 0.3f;
        [SerializeField] private float timeExist = 3f;
        
        private float _leftTimeBeforeDamage;
        
        private void OnTriggerStay(Collider other)
        {
            if (_leftTimeBeforeDamage > 0)
                return;

            _leftTimeBeforeDamage = damageTakeInterval;
            
            var hitGameObject = other.gameObject;
            
            if (LayerMasks.Enemy == (LayerMasks.Enemy | (1 << hitGameObject.layer)))
            {
                var ai = hitGameObject.GetComponent<IDamagable>();
                ai?.ReceiveDamage(Damage);
            }
        }

        public override void ResetProjectile()
        {
            _leftTimeBeforeDamage = 0;
            
            base.ResetProjectile();
        }
        
        protected override void OnTimerUpdating(float deltaTime)
        {
            base.OnTimerUpdating(deltaTime);
            
            if (_leftTimeBeforeDamage > 0)
            {
                _leftTimeBeforeDamage -= deltaTime;
            }
        }
    }
}