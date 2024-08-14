using System;
using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using R3;
using Services.InputService;
using UnityEngine;
using Views.Impl;
using Views.Modules.Impl;
using Views.Modules.Impl.Animations;

namespace Game.Controllers.Weapon
{
    [Install(EExecutionPriority.Normal, 55)]
    public class AttackController : IUpdatable
    {
        private readonly PlayerCharacterView _playerView;
        private readonly IInputService _inputService;
        private readonly WeaponModule _weaponModule;
        private readonly ShootAnimationModule _shootAnimationModule;

        private float _shootCooldown;
        
        public AttackController(
            IInputService inputService,
            IGameFieldProvider gameFieldProvider
        )
        {
            _inputService = inputService;
            var playerView = gameFieldProvider.GameField.PlayerCharacterView;
            _weaponModule = playerView.GetModule<WeaponModule>();
            _shootAnimationModule = playerView.GetModule<ShootAnimationModule>();
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
            
            _shootAnimationModule.PlayShootAnimation();
            
            _shootCooldown = _weaponModule.FireRate;

            Observable.Timer(TimeSpan.FromSeconds(0.3f)).Subscribe(_ =>
            {
                _shootAnimationModule.StopShootAnimation();
            });
        }
    }
}