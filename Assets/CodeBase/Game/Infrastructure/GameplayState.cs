using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
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
        private readonly IPlayerHitsWriter _playerHitsWriter;

        public GameplayState(IGameStateMachine stateMachine,
            IWindowFsm windowFsm,
            IWindowResolve windowResolve,
            IPlayerScoreWriter playerScoreWriter,
            IPlayerHitsWriter playerHitsWriter)
        {
            _stateMachine = stateMachine;
            _windowResolve = windowResolve;
            _windowFsm = windowFsm;
            _playerScoreWriter = playerScoreWriter;
        }

        public void OnEnter()
        {
            _playerScoreWriter.Score = 0;
            WindowResolve();
            _windowFsm.OpenWindow(typeof(GameplayView), true);
        }

        public void OnExit()
        {

        }

        private void WindowResolve()
        {
            _windowResolve.CleanUp();
            _windowResolve.Set<GameplayView>();
            //_windowResolve.Set<TestingToolsView>();
        }
    }
}