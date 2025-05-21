using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Infrastructure.States.Gameplay;
using Game.Logic.Player;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class DefeatViewModel : AbstractViewModel
    {
        public event Action<string> OnOpen;

        private readonly IGameStateMachine _gameFSM;
        private readonly IPlayerScoreReader _playerScoreReader;

        protected override Type Window => typeof(DefeatView);

        public string Score;

        public DefeatViewModel(IWindowFsm windowFsm,
            IGameStateMachine gameFSM, IPlayerScoreReader playerScoreReader)
            : base(windowFsm)
        {
            _gameFSM = gameFSM;
            _playerScoreReader = playerScoreReader;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            if (uiWindow != Window) return;
            OnOpen?.Invoke(_playerScoreReader.Score.ToString());
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