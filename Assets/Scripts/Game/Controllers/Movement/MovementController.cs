using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using Services.InputService;
using Views.Modules.Impl;

namespace Game.Controllers.Movement
{
    [Install(EExecutionPriority.Normal, 50)]
    public class MovementController : IUpdatable
    {
        private readonly IInputService _inputService;
        private readonly MoveModule _moveModule;
        
        public MovementController(
            IInputService inputService, 
            IGameFieldProvider gameFieldProvider
        )
        {
            _inputService = inputService;
            
            var playerCharacterView = gameFieldProvider.GameField.PlayerCharacterView;
            _moveModule = playerCharacterView.GetModule<MoveModule>();
        }
        
        public void Update()
        {
            if (!_inputService.IsMoving) return;
            
            _moveModule.MoveCharacter(_inputService.MoveDirection);
        }
    }
}