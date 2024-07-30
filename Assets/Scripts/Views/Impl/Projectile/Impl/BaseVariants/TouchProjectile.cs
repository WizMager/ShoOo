using UnityEngine;
using Utils.LayerMask;

namespace Views.Impl.Projectile.Impl.BaseVariants
{
    public class TouchProjectile : AProjectileView
    {
        [SerializeField] protected Rigidbody projectileRigidbody;
        
        private void OnTriggerEnter(Collider other)
        {
            var hitGameObject = other.gameObject;
            
            if (LayerMasks.Enemy == (LayerMasks.Enemy | (1 << hitGameObject.layer)))
            {
                var ai = hitGameObject.GetComponent<IDamagable>();
                
                ai?.ReceiveDamage(Damage);
                
                OnTouchAdditionalActions();
            }
        }

        protected virtual void OnTouchAdditionalActions()
        {
        }
        
        public override void ResetProjectile()
        {
            projectileRigidbody.velocity = Vector3.zero;
            
            base.ResetProjectile();
        }
    }
}