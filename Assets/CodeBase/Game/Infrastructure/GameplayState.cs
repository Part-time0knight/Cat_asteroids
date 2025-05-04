using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Enemy;
using Game.Logic.Player;
using Game.Presentation.View;

namespace Game.Infrastructure
{
    public class GameplayState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowResolve _windowResolve;
        private readonly IWindowFsm _windowFsm;
        private readonly IPlayerScoreWriter _playerScoreWriter;
        private readonly PlayerHandler.Pool _playerSpawner;
        private readonly EnemySpawner _enemySpawner;

        private PlayerHandler _player;

        public GameplayState(IGameStateMachine stateMachine,
            PlayerHandler.Pool playerSpawner,
            EnemySpawner enemySpawner,
            IWindowFsm windowFsm,
            IWindowResolve windowResolve,
            IPlayerScoreWriter playerScoreWriter)
        {
            _stateMachine = stateMachine;
            _windowResolve = windowResolve;
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
            WindowResolve();
            _windowFsm.OpenWindow(typeof(GameplayView), true);
        }

        public void OnExit()
        {
            _playerSpawner.Despawn(_player);
            _enemySpawner.StopSpawn();
            _windowFsm.CloseWindow();
        }

        private void WindowResolve()
        {
            _windowResolve.CleanUp();
            _windowResolve.Set<GameplayView>();
            //_windowResolve.Set<TestingToolsView>();
        }
    }
}