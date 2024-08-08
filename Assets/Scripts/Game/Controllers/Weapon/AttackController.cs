using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using Services.InputService;
using UnityEngine;
using Views.Impl;
using Views.Modules.Impl;

namespace Game.Controllers.Weapon
{
    [Install(EExecutionPriority.Normal, 55)]
    public class AttackController : IUpdatable
    {
        private readonly PlayerCharacterView _playerView;
        private readonly IInputService _inputService;
        private readonly WeaponModule _weaponModule;

        private float _shootCooldown;
        
        public AttackController(
            IInputService inputService,
            IGameFieldProvider gameFieldProvider
        )
        {
            _inputService = inputService;
            var playerView = gameFieldProvider.GameField.PlayerCharacterView;
            _weaponModule = playerView.GetModule<WeaponModule>();
        }
        
        public void Update()
        {
            if (_shootCooldown > 0)
            {
                _shootCooldown -= Time.deltaTime;
                
                return;
            }
            
            if (!_inputService.IsAttack) return;
            
            _weaponModule.Shoot();
            _shootCooldown = _weaponModule.FireRate;
        }
    }
}