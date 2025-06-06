using Game.Presentation.ViewModel;
using Core.MVVM.View;
using UnityEngine;
using System;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace Game.Presentation.View
{
    public class DefeatView : AbstractPayloadView<DefeatViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(DefeatViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.OnOpen += UpdateScore;
            _settings.Restart.onClick.AddListener(_viewModel.InvokeRestart);
            _settings.MainMenu.onClick.AddListener(_viewModel.InvokeMainMenu);
            _settings.LeaderBoard.onClick.AddListener(_viewModel.InvokeLeaderBoard);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _viewModel.OnOpen -= UpdateScore;
            _settings.Restart.onClick.RemoveListener(_viewModel.InvokeRestart);
            _settings.MainMenu.onClick.RemoveListener(_viewModel.InvokeMainMenu);
            _settings.LeaderBoard.onClick.RemoveListener(_viewModel.InvokeLeaderBoard);
        }

        private void UpdateScore(string score, string maxScore)
        {
            _settings.ScoreCountText.text = score;
            _settings.ScoreMaxCountText.text = maxScore;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public TMP_Text ScoreCountText { get; private set; }
            [field: SerializeField] public TMP_Text ScoreMaxCountText { get; private set; }
            [field: SerializeField] public Button Restart { get; private set; }
            [field: SerializeField] public Button LeaderBoard { get; private set; }
            [field: SerializeField] public Button MainMenu { get; private set; }
        }
    }
}