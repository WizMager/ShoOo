using System;
using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using R3;
using Services.InputService;
using Views.Modules.Impl;

namespace Game.Controllers.Weapon
{
    [Install(EExecutionPriority.Normal, 70)]
    public class ReloadingController : IDisposable
    {
        private readonly WeaponModule _weaponModule;
        private readonly IDisposable _disposable;

        public ReloadingController(
            IInputService inputService,
            IGameFieldProvider gameFieldProvider)
        {
            var playerView = gameFieldProvider.GameField.PlayerCharacterView;
            _weaponModule = playerView.GetModule<WeaponModule>();
            
            _disposable = inputService.Reloading.Subscribe(_ => OnReloading());
        }

        private void OnReloading()
        {
            _weaponModule.Reload();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}