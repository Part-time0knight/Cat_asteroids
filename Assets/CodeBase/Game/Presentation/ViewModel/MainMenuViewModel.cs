using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Infrastructure.States.Menu;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class MainMenuViewModel : AbstractViewModel
    {
        private readonly IGameStateMachine _gameFsm;

        protected override Type Window => typeof(MainMenuView);

        public MainMenuViewModel(IWindowFsm windowFsm, IGameStateMachine gameFsm) : base(windowFsm)
        {
            _gameFsm = gameFsm;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        public void StartGame()
        {
            _gameFsm.Enter<LoadGameplay>();
        }

        public void OpenControlWindow()
        {

        }

        public void OpenSettingsWindow()
        {

        }
        
        public void OpenLeaderBoardWindow() 
        {

        }

        public void OpenAuthorsWindow()
        {

        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
        }
    }
}