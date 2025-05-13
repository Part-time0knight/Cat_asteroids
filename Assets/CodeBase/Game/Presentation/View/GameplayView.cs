using Core.MVVM.View;
using Game.Domain.Dto;
using Game.Presentation.Elements;
using Game.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class GameplayView : AbstractPayloadView<GameplayViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(GameplayViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.OnUpdate += InvokeUpdate;
            _viewModel.OnDamaged += InvokeTakeDamage;
            _viewModel.OnScoresShow += InvokeScoresShow;
            _settings.GoToPause.onClick.AddListener(_viewModel.InvokePause);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _viewModel.OnUpdate -= InvokeUpdate;
            _viewModel.OnDamaged += InvokeTakeDamage;
            _viewModel.OnScoresShow -= InvokeScoresShow;
            _settings.GoToPause.onClick.RemoveListener(_viewModel.InvokePause);
        }

        private void InvokeUpdate(GameplayDto dto)
        {
            _settings.ScoreCountText.text = dto.Score;
            _settings.HitsViewer.SetHits(dto.Hits);
            _settings.HitsViewer.SetPanelActive(dto.ShowHits);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_settings.ScoreCountText.rectTransform);
        }

        private void InvokeTakeDamage()
        {
            _settings.ShakeAnimation.Play();
        }

        private void InvokeScoresShow(List<GameplayViewModel.ScoreData> scores)
        {
            _settings.ScoreViewer.Show(scores);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public TMP_Text ScoreTitleText { get; private set; }
            [field: SerializeField] public TMP_Text ScoreCountText { get; private set; }
            [field: SerializeField] public Button GoToPause { get; private set; }
            [field: SerializeField] public ScoreViewer ScoreViewer { get; private set; }
            [field: SerializeField] public HitsViewer HitsViewer { get; private set; }
            [field: SerializeField] public ShakeAnimation ShakeAnimation { get; private set; }
        }
    }
}