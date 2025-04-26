using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Logic.Handlers;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class GameplayButtonsViewModel : AbstractViewModel
    {
        private readonly IPauseHandler _pause;

        protected override Type Window => typeof(GameplayButtonsView);

        public GameplayButtonsViewModel(IWindowFsm windowFsm, IPauseHandler pause) : base(windowFsm)
        {
            _pause = pause;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        public void OpenTestingToolsWindow()
        {
            _windowFsm.OpenWindow(typeof(TestingToolsView), inHistory: true);
        }

        public void OpenMenuPauseWindow()
        {
            _pause.Active = true;
            _windowFsm.OpenWindow(typeof(MenuPauseView), inHistory: true);
        }
    }
}