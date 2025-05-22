using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Infrastructure.States.Gameplay;
using Game.Logic.Player;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{

    public class PowerUpViewModel : AbstractViewModel
    {
        public event Action<int, string> OnOpen;

        private readonly IPlayerHitsReader _hitsReader;
        private readonly IPlayerScoreReader _scoreReader;
        private readonly IGameStateMachine _gameFsm;

        protected override Type Window => typeof(PowerUpView);

        public PowerUpViewModel(IWindowFsm windowFsm,
            IGameStateMachine gameFsm,
            IPlayerScoreReader scoreReader,
            IPlayerHitsReader hitsReader) : base(windowFsm)
        {
            _hitsReader = hitsReader;
            _scoreReader = scoreReader;
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

        public void InvokeContinue()
        {
            _gameFsm.Enter<GameplayState>();
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            if (uiWindow != Window) return;
            OnOpen?.Invoke(_hitsReader.Hits, _scoreReader.Score.ToString());
        }
    }
}