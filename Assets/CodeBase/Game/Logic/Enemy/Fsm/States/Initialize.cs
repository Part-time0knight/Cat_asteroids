using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowResolve _windowResolve;


        public Initialize(IGameStateMachine stateMachine,
            IWindowResolve windowResolve)
        {
            _stateMachine = stateMachine;
            _windowResolve = windowResolve;
        }

        public void OnEnter()
        {
            WindowResolve();
            _stateMachine.Enter<Run>();
        }

        public void OnExit()
        {

        }

        private void WindowResolve()
        {
            _windowResolve.Set<EnemyView>();
        }
    }
}