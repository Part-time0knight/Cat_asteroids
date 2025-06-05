using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;


namespace Game.Infrastructure.States.Gameplay
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
            //UnityEngine.Debug.Log("Enter state Initialize");
            WindowResolve();
            _stateMachine.Enter<Start>();
        }

        public void OnExit()
        {
        }

        private void WindowResolve()
        {
            _windowResolve.CleanUp();
            _windowResolve.Set<StartView>();
            _windowResolve.Set<GameplayView>();
            _windowResolve.Set<PauseView>();
            _windowResolve.Set<DefeatView>();
            _windowResolve.Set<LoadView>();
            _windowResolve.Set<ControlWindowView>();
            _windowResolve.Set<SettingsWindowView>();
            _windowResolve.Set<LeaderBoardView>();
            _windowResolve.Set<PowerUpView>();
            _windowResolve.Set<BundleChooseView>();
        }
    }
}