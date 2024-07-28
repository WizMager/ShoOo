using R3;
using UnityEngine;
using Utils.LayerMask;

namespace Views.Impl.Projectile
{
    public class OneTouchProjectile : AProjectileView
    {
        [SerializeField] protected Rigidbody projectileRigidbody;
        
        private readonly ReactiveCommand<Unit> _projectileTouchedTarget = new ();
        
        public Observable<Unit> ProjectileTouchedTarget => _projectileTouchedTarget;
        
        private void OnTriggerEnter(Collider other)
        {
            var hitGameObject = other.gameObject;
            
            if (LayerMasks.Enemy == (LayerMasks.Enemy | (1 << hitGameObject.layer)))
            {
                var ai = hitGameObject.GetComponent<IDamagable>();
                
                ai?.ReceiveDamage(Damage);
                
                _projectileTouchedTarget.Execute(default);
            }
        }
        
        public override void ResetProjectile()
        {
            projectileRigidbody.velocity = Vector3.zero;
            
            base.ResetProjectile();
        }
    }
}