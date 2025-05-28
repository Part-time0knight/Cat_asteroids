using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Infrastructure.States.Gameplay;
using Game.Logic.Handlers;
using Game.Logic.Player;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{

    public class PowerUpViewModel : AbstractViewModel
    {
        public event Action<Dto> OnOpen;

        private readonly IPlayerHitsReader _hitsReader;
        private readonly IPlayerScoreReader _scoreReader;
        private readonly IGameStateMachine _gameFsm;
        private readonly DifficultHandler _difficultHandler;
        private readonly Dto _dto = new();

        protected override Type Window => typeof(PowerUpView);

        public PowerUpViewModel(IWindowFsm windowFsm,
            IGameStateMachine gameFsm,
            IPlayerScoreReader scoreReader,
            IPlayerHitsReader hitsReader,
            DifficultHandler difficultHandler) : base(windowFsm)
        {
            _hitsReader = hitsReader;
            _scoreReader = scoreReader;
            _difficultHandler = difficultHandler;
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
            Update();
        }

        private void Update()
        {
            _dto.Hits = _hitsReader.Hits;
            _dto.Score = _scoreReader.Score.ToString();
            _dto.ScoreToNextLayer = _difficultHandler.NextStep.ToString();
            _dto.Layer = "#" + (_difficultHandler.CurrentDifficult - 1);
            OnOpen?
                .Invoke(_dto);
        }

        public class Dto
        {
            public int Hits;
            public string Score;
            public string ScoreToNextLayer;
            public string Layer;
        }
    }
}