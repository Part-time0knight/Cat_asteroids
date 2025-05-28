using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Enemy.Spawner;
using Game.Logic.Player;
using Game.Presentation.View;

namespace Game.Infrastructure.States.Gameplay
{
    public class PowerUp : IState
    {
        private readonly ISpawnerService _spawnerService;
        private readonly PlayerFacade.Pool _playerPool;
        private readonly IWindowFsm _windowFsm;

        private PlayerFacade _player;

        public PowerUp(ISpawnerService spawnerService,
            PlayerFacade.Pool pool, IWindowFsm windowFsm) 
        {
            _spawnerService = spawnerService;
            _playerPool = pool;
            _windowFsm = windowFsm;

        }

        public void OnEnter()
        {
            _spawnerService.KillAll();
            _player = _playerPool.Spawn();
            _player.Pause = true;
            _windowFsm.OpenWindow(typeof(PowerUpView), true);
        }

        public void OnExit()
        {
            _player.Pause = false;
            _playerPool.Despawn(_player);
            _windowFsm.CloseWindow();
        }
    }
}