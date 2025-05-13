using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;

namespace Infrastructure.States.Menu
{
    public class MainMenu : IState
    {
        private readonly IWindowFsm _windowFsm;

        public MainMenu(IWindowFsm windowFsm)
        {
            _windowFsm = windowFsm;
        }

        public void OnEnter()
        {
            _windowFsm.OpenWindow(typeof(MainMenuView), inHistory: true);
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow();
        }

    }
}