using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Enemy;
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

        private PlayerHandler _player;

        public Pause(IWindowFsm windowFsm,
            PlayerHandler.Pool playerPool,
            EnemySpawner enemySpawner)
        {
            _windowFsm = windowFsm;
            _playerPool = playerPool;
            _enemySpawner = enemySpawner;
        }

        public void OnEnter()
        {
            Debug.Log("Enter state Pause");
            _player = _playerPool.Spawn();
            _player.Pause = true;
            _enemySpawner.Pause();
            _windowFsm.OpenWindow(typeof(PauseView), true);
            
        }

        public void OnExit()
        {
            _player.Pause = false;
            _playerPool.Despawn(_player);
            _enemySpawner.Continue();
            _windowFsm.CloseWindow();
        }
    }
}