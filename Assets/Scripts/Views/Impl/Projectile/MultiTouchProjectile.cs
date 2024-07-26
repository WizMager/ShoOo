using R3;
using UnityEngine;
using Utils.LayerMask;
using Views.Impl.Ai;

namespace Views.Impl.Projectile
{
    public class MultiTouchProjectile : AProjectileView
    {
        [SerializeField] private float damageTakeCooldown = 1f;
        private float _leftTime;
        private readonly ReactiveCommand<AAiView> _aiHitReactiveCommand = new ();
        
        public Observable<AAiView> OnAiHit => _aiHitReactiveCommand;
        
        private void OnTriggerStay(Collider other)
        {
            if (_leftTime > 0)
            {
                _leftTime -= Time.deltaTime;
                return;
            }

            _leftTime = damageTakeCooldown;
            
            var hitGameObject = other.gameObject;
            
            if (LayerMasks.Enemy == (LayerMasks.Enemy | (1 << hitGameObject.layer)))
            {
                var aiView = hitGameObject.GetComponent<AAiView>();
                
                _aiHitReactiveCommand.Execute(aiView);
            }
        }

        public override void ResetProjectile()
        {
            _leftTime = 0;
            
            base.ResetProjectile();
        }
    }
}