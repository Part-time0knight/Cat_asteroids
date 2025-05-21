using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Infrastructure.States.Gameplay;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class DefeatViewModel : AbstractViewModel
    {
        private readonly IGameStateMachine _gameFSM;

        protected override Type Window => typeof(DefeatView);

        public DefeatViewModel(IWindowFsm windowFsm,
            IGameStateMachine gameFSM)
            : base(windowFsm)
        {
            _gameFSM = gameFSM;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        public void InvokeRestart()
        {
            _gameFSM.Enter<Start>();
        }

        public void InvokeMainMenu()
        {
            _gameFSM.Enter<LoadMenu>();
        }

        public void InvokeLeaderBoard() 
        {
            _windowFsm.OpenWindow(typeof(LeaderBoardView), true);
        }

    }
}