using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using Services.InputService;
using UnityEngine;
using Views.Impl;
using Views.Modules.Impl;

namespace Game.Controllers.Controls
{
    [Install(EExecutionPriority.Normal, 45)]
    public class AimingController : IUpdatable
    {
        private readonly IInputService _inputService;
        private readonly AimingModule _aimingModule;

        public AimingController(
            IInputService inputService,
            IGameFieldProvider gameFieldProvider
        )
        {
            _inputService = inputService;
            
            var playerCharacterView = gameFieldProvider.GameField.PlayerCharacterView;
            _aimingModule = playerCharacterView.GetModule<AimingModule>();
        }

        public void Update()
        {
            if (!_inputService.IsAiming) return;

            var camera = Camera.main;
            if (camera == null) return;
            
            var mousePosition = _inputService.AimPosition;
            var mouseWorldPosition = camera.ScreenToWorldPoint(mousePosition);
            mouseWorldPosition.z = 0;
            _aimingModule.RotateCharacter(mouseWorldPosition);
        }
    }
}