using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Domain.Dto;
using Game.Logic.Player;
using Game.Presentation.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation.ViewModel
{
    public class GameplayViewModel : AbstractViewModel
    {
        public event Action<GameplayDto> OnUpdate;
        public event Action<List<ScoreData>> OnScoresShow;

        private readonly IPlayerScoreReader _scoreReader;
        private readonly IPlayerHitsReader _hitsReader;
        private readonly GameplayDto _dto = new();
        private readonly List<ScoreData> _scoresToView = new(); 

        protected override Type Window => typeof(GameplayView);

        public GameplayViewModel(IWindowFsm windowFsm,
            IPlayerScoreReader scoreReader,
            IPlayerHitsReader hitsReader) : base(windowFsm)
        {
            _scoreReader = scoreReader;
            _hitsReader = hitsReader;
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
            if (Window != uiWindow)
                return;
            InvokeUpdate();
            _scoreReader.OnScoreUpdate += InvokeUpdate;
            _scoreReader.OnScoreAdd += InvokeShowScores;
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            if (Window != uiWindow)
                return;
            _scoreReader.OnScoreUpdate -= InvokeUpdate;
            _scoreReader.OnScoreAdd -= InvokeShowScores;
        }

        private void InvokeUpdate()
        {
            _dto.Score = _scoreReader.Score.ToString();
            _dto.Hits = _hitsReader.Hits;
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

        public struct ScoreData
        {
            public string Score;
            public Vector2 Position;
        }
    }
}