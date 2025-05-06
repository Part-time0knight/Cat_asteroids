using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Enemy;
using Game.Logic.Player;
using Game.Presentation.View;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.States
{
    public class GameplayState : IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly IPlayerScoreWriter _playerScoreWriter;
        private readonly PlayerHandler.Pool _playerSpawner;
        private readonly EnemySpawner _enemySpawner;

        private PlayerHandler _player;

        public GameplayState(PlayerHandler.Pool playerSpawner,
            EnemySpawner enemySpawner,
            IWindowFsm windowFsm,
            IPlayerScoreWriter playerScoreWriter)
        {
            _windowFsm = windowFsm;
            _playerScoreWriter = playerScoreWriter;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
        }

        public void OnEnter()
        {
            Debug.Log("Enter state GameplayState");
            _playerScoreWriter.Score = 0;
            _player = _playerSpawner.Spawn();
            _enemySpawner.BeginSpawn();
            _windowFsm.OpenWindow(typeof(GameplayView), true);
            
        }

        public void OnExit()
        {
            Debug.Log("Exit state GameplayState");
            _playerSpawner.Despawn(_player);
            _enemySpawner.StopSpawn();
            _windowFsm.CloseWindow();
        }
    }
}