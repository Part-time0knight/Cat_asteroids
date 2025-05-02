using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;

namespace Game.Logic.Player.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowResolve _windowResolve;
        private readonly IWindowFsm _windowFsm;

        public Initialize(IGameStateMachine stateMachine,
            IWindowFsm windowFsm,
            IWindowResolve windowResolve)
        {
            _stateMachine = stateMachine;
            _windowResolve = windowResolve;
            _windowFsm = windowFsm;
        }

        public void OnEnter()
        {
            WindowResolve();
            //_windowFsm.OpenWindow(typeof(PlayerView), inHistory: false);
            _stateMachine.Enter<Idle>();
        }

        public void OnExit()
        {
            
        }

        private void WindowResolve()
        {
            //_windowResolve.Set<PlayerView>();
        }
    }
}