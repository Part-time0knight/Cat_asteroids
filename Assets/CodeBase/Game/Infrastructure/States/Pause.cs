using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Effects.Explosion;
using Game.Logic.Enemy;
using Game.Logic.Handlers;
using Game.Logic.Player;
using Game.Presentation.View;
using UnityEngine;

namespace Game.Infrastructure.States
{
    public class Pause : IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly PlayerHandler.Pool _playerPool;
        private readonly EnemySpawner _enemySpawner;
        private readonly PauseInputHandler _pauseInputHandler;
        private readonly IGameStateMachine _gameFsm;
        private readonly ExplosionSpawner _explosionSpawner;

        private PlayerHandler _player;

        public Pause(IGameStateMachine gameFsm,
            IWindowFsm windowFsm,
            PlayerHandler.Pool playerPool,
            EnemySpawner enemySpawner,
            PauseInputHandler pauseInputHandler,
            ExplosionSpawner explosionSpawner)
        {
            _gameFsm = gameFsm;
            _windowFsm = windowFsm;
            _playerPool = playerPool;
            _enemySpawner = enemySpawner;
            _explosionSpawner = explosionSpawner;
            _pauseInputHandler = pauseInputHandler;
        }

        public void OnEnter()
        {
            Debug.Log("Enter state Pause");
            _player = _playerPool.Spawn();
            _player.Pause = true;
            _enemySpawner.Pause();
            _explosionSpawner.Pause();
            _windowFsm.OpenWindow(typeof(PauseView), true);
            _pauseInputHandler.OnPressPause += InvokePressPause;
        }

        public void OnExit()
        {
            _player.Pause = false;
            _playerPool.Despawn(_player);
            _enemySpawner.Continue();
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