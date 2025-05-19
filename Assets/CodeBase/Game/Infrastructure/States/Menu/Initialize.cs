using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;
using Infrastructure.States.Menu;

namespace Game.Infrastructure.States.Menu
{
    public class Initialize : IState
    {
        private readonly IWindowResolve _windowResolve;
        private readonly IGameStateMachine _stateMachine;

        public Initialize(IWindowResolve windowResolve,
            IGameStateMachine stateMachine)
        {
            _windowResolve = windowResolve;
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            WindowResolve();
            _stateMachine.Enter<MainMenu>();
        }

        public void OnExit()
        {
            
        }

        private void WindowResolve()
        {
            _windowResolve.CleanUp();
            _windowResolve.Set<MainMenuView>();
            _windowResolve.Set<LoadView>();
            _windowResolve.Set<ControlWindowView>();
        }
    }
}