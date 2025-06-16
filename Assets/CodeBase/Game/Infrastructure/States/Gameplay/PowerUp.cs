using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Enemy.Spawner;
using Game.Logic.Player;
using Game.Logic.Services.Mutators;
using Game.Presentation.View;

namespace Game.Infrastructure.States.Gameplay
{
    public class PowerUp : IState
    {
        private readonly ISpawnerService _spawnerService;
        private readonly PlayerFacade.Pool _playerPool;
        private readonly IWindowFsm _windowFsm;
        private readonly BundleService _bundleService;
        private readonly IMutatorPause _mutatorPause;

        private PlayerFacade _player;

        public PowerUp(ISpawnerService spawnerService,
            PlayerFacade.Pool pool,
            IWindowFsm windowFsm,
            BundleService bundleService,
            IMutatorPause mutatorPause) 
        {
            _spawnerService = spawnerService;
            _playerPool = pool;
            _windowFsm = windowFsm;
            _bundleService = bundleService;
            _mutatorPause = mutatorPause;
        }

        public void OnEnter()
        {
            _mutatorPause.SetPause(true);
            _spawnerService.KillAll();
            _player = _playerPool.Spawn();
            _player.Pause = true;
            _bundleService.GenerateBundles();
            _windowFsm.OpenWindow(typeof(PowerUpView), true);
        }

        public void OnExit()
        {
            _mutatorPause.SetPause(false);
            _player.Pause = false;
            _playerPool.Despawn(_player);
            _windowFsm.CloseWindow();
        }
    }
}