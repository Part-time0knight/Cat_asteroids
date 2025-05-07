using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Enemy;
using Game.Logic.Handlers;
using Game.Logic.Player;
using Game.Presentation.View;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.States
{
    public class GameplayState : IState
    {
        private readonly IWindowFsm _windowFsm;
        
        private readonly PlayerHandler.Pool _playerSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly PauseInputHandler _pauseInputHandler;
        private readonly IGameStateMachine _gameFsm;

        private PlayerHandler _player;

        public GameplayState(IGameStateMachine gameFsm,
            PlayerHandler.Pool playerSpawner,
            EnemySpawner enemySpawner,
            PauseInputHandler pauseInputHandler,
            IWindowFsm windowFsm)
        {
            _gameFsm = gameFsm;
            _windowFsm = windowFsm;
            _pauseInputHandler = pauseInputHandler;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
        }

        public void OnEnter()
        {
            Debug.Log("Enter state GameplayState");
            
            _player = _playerSpawner.Spawn();
            _enemySpawner.BeginSpawn();
            _windowFsm.OpenWindow(typeof(GameplayView), true);
            _pauseInputHandler.OnPressPause += InvokePressPause;
        }

        public void OnExit()
        {
            _playerSpawner.Despawn(_player);
            _enemySpawner.StopSpawn();
            _windowFsm.CloseWindow();
            _pauseInputHandler.OnPressPause -= InvokePressPause;
        }

        private void InvokePressPause()
        {
            _gameFsm.Enter<Pause>();
        }
    }
}