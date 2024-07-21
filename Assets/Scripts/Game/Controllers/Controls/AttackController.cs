using Game.Bootstrap.Interfaces;
using Generator;
using Providers.GameFieldProvider;
using Services.InputService;
using Views.Impl;

namespace Game.Controllers.Controls
{
    [Install(EExecutionPriority.Normal, 55)]
    public class AttackController : IUpdatable
    {
        private readonly PlayerCharacterView _playerView;
        private readonly IInputService _inputService;
        
        public AttackController(
            IInputService inputService,
            IGameFieldProvider gameFieldProvider
        )
        {
            _inputService = inputService;
            _playerView = gameFieldProvider.GameField.PlayerCharacterView;
        }
        
        public void Update()
        {
            
        }
    }
}