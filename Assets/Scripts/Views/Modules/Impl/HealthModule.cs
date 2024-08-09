using R3;
using UnityEngine;

namespace Views.Modules.Impl
{
    public class HealthModule : AModule, IDamagable
    {
        public Observable<Unit> AiExistEnded => _aiExistEndedCommand;
        
        [SerializeField] private float maxHealth;

        private float _currentHealth;
        private readonly ReactiveCommand<Unit> _aiExistEndedCommand = new ();

        public override void Initialize(AView view, CompositeDisposable disposable)
        {
            base.Initialize(view, disposable);

            _currentHealth = maxHealth;
        }

        public void ReceiveDamage(float damage)
        {
            _currentHealth -= damage;
            Debug.Log($"Ai {gameObject.name} receive damage: {damage}! Current health: {_currentHealth}");
            if (_currentHealth <= 0)
            {
                _aiExistEndedCommand.Execute(default);
            }
        }
    }
}