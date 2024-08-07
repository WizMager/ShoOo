using System;
using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using R3;
using Services.InputService;
using Views.Modules.Impl;

namespace Game.Controllers.Controls
{
    [Install(EExecutionPriority.Normal, 70)]
    public class ReloadingController : IController, IDisposable
    {
        private readonly AttackModule _attackModule;
        private readonly IDisposable _disposable;

        public ReloadingController(
            IInputService inputService,
            IGameFieldProvider gameFieldProvider)
        {
            var playerView = gameFieldProvider.GameField.PlayerCharacterView;
            _attackModule = playerView.GetModule<AttackModule>();
            
            _disposable = inputService.Reloading.Subscribe(_ => OnReloading());
        }

        private void OnReloading()
        {
            _attackModule.Reloading();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}