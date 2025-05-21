using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class LeaderBoardViewModel : AbstractViewModel
    {
        protected override Type Window => typeof(LeaderBoardView);

        public LeaderBoardViewModel(IWindowFsm windowFsm) : base(windowFsm)
        {
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }
    }
}