using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Effects.Explosion;
using Game.Logic.Enemy.Spawner;
using Game.Logic.Handlers;
using Game.Logic.Player;
using Game.Logic.Services.Mutators;
using Game.Presentation.View;
using UnityEngine;

namespace Game.Infrastructure.States.Gameplay
{
    public class MenuPause : IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly PlayerFacade.Pool _playerPool;
        private readonly ISpawnerService _enemySpawner;
        private readonly PauseInputHandler _pauseInputHandler;
        private readonly IGameStateMachine _gameFsm;
        private readonly ExplosionSpawner _explosionSpawner;
        private readonly IMutatorPause _mutatorPause;

        private PlayerFacade _player;

        public MenuPause(IGameStateMachine gameFsm,
            IWindowFsm windowFsm,
            PlayerFacade.Pool playerPool,
            ISpawnerService enemySpawner,
            PauseInputHandler pauseInputHandler,
            ExplosionSpawner explosionSpawner,
            IMutatorPause mutatorPause)
        {
            _gameFsm = gameFsm;
            _windowFsm = windowFsm;
            _playerPool = playerPool;
            _enemySpawner = enemySpawner;
            _mutatorPause = mutatorPause;
            _explosionSpawner = explosionSpawner;
            _pauseInputHandler = pauseInputHandler;
        }

        public void OnEnter()
        {
            Debug.Log("Enter state Menu Pause");
            _mutatorPause.SetPause(true);
            _player = _playerPool.Spawn();
            _player.Pause = true;
            _enemySpawner.PauseAll();
            _explosionSpawner.Pause();
            _windowFsm.OpenWindow(typeof(PauseView), true);
            _pauseInputHandler.OnPressPause += InvokePressPause;
        }

        public void OnExit()
        {
            _mutatorPause.SetPause(false);
            _player.Pause = false;
            _playerPool.Despawn(_player);
            _enemySpawner.ContinueAll();
            _explosionSpawner.Continue();
            _windowFsm.CloseWindow();
            _pauseInputHandler.OnPressPause -= InvokePressPause;
        }

        private void InvokePressPause()
        {
            _gameFsm.Enter<GameplayState>();
        }
    }
}