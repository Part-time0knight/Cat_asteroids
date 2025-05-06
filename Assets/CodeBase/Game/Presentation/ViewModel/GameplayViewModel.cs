using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Domain.Dto;
using Game.Infrastructure.States;
using Game.Logic.Player;
using Game.Presentation.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation.ViewModel
{
    public class GameplayViewModel : AbstractViewModel
    {
        public event Action OnDamaged;
        public event Action<GameplayDto> OnUpdate;
        public event Action<List<ScoreData>> OnScoresShow;

        private readonly GameplayDto _dto = new();
        private readonly IPlayerHitsReader _hitsReader;
        private readonly IPlayerScoreReader _scoreReader;
        private readonly IGameStateMachine _gameFsm;
        private readonly List<ScoreData> _scoresToView = new(); 

        protected override Type Window => typeof(GameplayView);

        public GameplayViewModel(IGameStateMachine gameFsm,
            IWindowFsm windowFsm,
            IPlayerScoreReader scoreReader,
            IPlayerHitsReader hitsReader) : base(windowFsm)
        {
            _scoreReader = scoreReader;
            _hitsReader = hitsReader;
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

        public void InvokePause()
        {
            _gameFsm.Enter<Pause>();
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            if (Window != uiWindow)
                return;
            InvokeCountsUpdate();

            _scoreReader.OnScoreUpdate += InvokeCountsUpdate;
            _hitsReader.OnHitsUpdate += InvokeCountsUpdate;
            _hitsReader.OnDamaged += InvokeDamaged;
            _scoreReader.OnScoreAdd += InvokeShowScores;
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            if (Window != uiWindow)
                return;
            _scoreReader.OnScoreUpdate -= InvokeCountsUpdate;
            _hitsReader.OnHitsUpdate -= InvokeCountsUpdate;
            _hitsReader.OnDamaged -= InvokeDamaged;
            _scoreReader.OnScoreAdd -= InvokeShowScores;
        }

        private void InvokeCountsUpdate()
        {
            _dto.Score = _scoreReader.Score.ToString();
            _dto.Hits = _hitsReader.Hits;
            _dto.ShowHits = _hitsReader.Hits > 0 ? true : false;
            OnUpdate?.Invoke(_dto);
        }

        private void InvokeShowScores(int score, Vector2 position)
        {
            _scoresToView.Add(new() 
            {
                Score = "+" + score.ToString(),
                Position = Camera.main.WorldToScreenPoint(position),
            });
            OnScoresShow?.Invoke(_scoresToView);
            _scoresToView.Clear();
        }

        private void InvokeDamaged(bool isDamaged)
        {
            if (!isDamaged)
                return;
            OnDamaged?.Invoke();
        }



        public struct ScoreData
        {
            public string Score;
            public Vector2 Position;
        }
    }
}