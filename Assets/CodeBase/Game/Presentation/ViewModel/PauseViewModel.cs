using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Infrastructure.States.Gameplay;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class PauseViewModel : AbstractViewModel
    {
        protected override Type Window => typeof(PauseView);

        private readonly IGameStateMachine _gameFsm;

        public PauseViewModel(IGameStateMachine gameFsm,
            IWindowFsm windowFsm) : base(windowFsm)
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

        public void InvokeGoToMainMenu()
        {
            _gameFsm.Enter<LoadMenu>();
        }

        public void InvokeReturn()
        {
            _gameFsm.Enter<GameplayState>();
        }

        public void InvokeGoToControl()
        {
            _windowFsm.OpenWindow(typeof(ControlWindowView), true);
        }

        public void InvokeGoToSettings()
        {
            _windowFsm.OpenWindow(typeof(SettingsWindowView), true);
        }

        public void InvokeRestart()
        {
            _gameFsm.Enter<Start>();
        }
    }
}