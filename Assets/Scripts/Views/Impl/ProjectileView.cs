using R3;
using UnityEngine;
using Utils.LayerMask;
using Views.Impl.Ai;

namespace Views.Impl
{
    public class ProjectileView : AView //TODO: rework need abstract projectiles
    {
        [SerializeField] private Rigidbody projectileRigidbody;
        
        private readonly ReactiveCommand<AAiView> _aiHitReactiveCommand = new ();
        
        public Observable<AAiView> OnAiHit => _aiHitReactiveCommand;
        
        private void OnTriggerEnter(Collider other)
        {
            var hitGameObject = other.gameObject;
            
            if (LayerMasks.Enemy == (LayerMasks.Enemy | (1 << hitGameObject.layer)))
            {
                var aiView = hitGameObject.GetComponent<AAiView>();
                
                _aiHitReactiveCommand.Execute(aiView);
            }
        }

        public void ResetProjectile()
        {
            gameObject.SetActive(false);
        }

        public void ActivateProjectile(float projectileSpeed)
        {
            gameObject.SetActive(true);
            projectileRigidbody.AddForce(transform.forward * projectileSpeed, ForceMode.Acceleration);
        }
    }
}