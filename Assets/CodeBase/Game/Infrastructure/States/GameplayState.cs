using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Enemy;
using Game.Logic.Player;
using Game.Presentation.View;

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
            _playerScoreWriter.Score = 0;
            _player = _playerSpawner.Spawn();
            _enemySpawner.BeginSpawn();
            _windowFsm.OpenWindow(typeof(GameplayView), true);
            UnityEngine.Debug.Log("Enter state GameplayState");
        }

        public void OnExit()
        {
            _playerSpawner.Despawn(_player);
            _enemySpawner.StopSpawn();
            _windowFsm.CloseWindow();
        }
    }
}