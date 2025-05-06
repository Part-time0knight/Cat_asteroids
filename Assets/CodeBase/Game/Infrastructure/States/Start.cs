using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Player;
using Game.Presentation.View;

namespace Game.Infrastructure.States
{
    public class Start : IState
    {
        private readonly PlayerHandler.Pool _playerPool;
        private readonly IPlayerPositionReader _positionReader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWindowFsm _windowFsm;

        private PlayerHandler _player;

        public Start(IGameStateMachine gameStateMachine,
            PlayerHandler.Pool playerPool,
            IPlayerPositionReader positionReader,
            IWindowFsm windowFsm) 
        {
            _gameStateMachine = gameStateMachine;
            _positionReader = positionReader;
            _playerPool = playerPool;
            _windowFsm = windowFsm;
        }



        public void OnEnter()
        {
            UnityEngine.Debug.Log("Enter state Start");
            _player = _playerPool.Spawn();
            _player.ActiveShooting = false;
            _player.ResetPlayer();
            
            
            _windowFsm.OpenWindow(typeof(StartView), true);
            _positionReader.OnMove += InvokeMove;
            
        }

        public void OnExit()
        {
            _playerPool.Despawn(_player);
            _player.ActiveShooting = true;
            _windowFsm.CloseWindow();
            _positionReader.OnMove -= InvokeMove;
        }

        private void InvokeMove(bool value)
        {
            if (!value)
                return;
            _gameStateMachine.Enter<GameplayState>();
        }
    }
}