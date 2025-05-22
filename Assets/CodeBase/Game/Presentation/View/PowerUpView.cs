using Core.MVVM.View;
using Game.Presentation.Elements;
using Game.Presentation.ViewModel;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class PowerUpView : AbstractPayloadView<PowerUpViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(PowerUpViewModel viewModel)
        {
            base.Construct(viewModel);
            _settings.ExitButton.onClick.AddListener(_viewModel.InvokeContinue);
            _viewModel.OnOpen += InvokeOpen;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _settings.ExitButton.onClick.RemoveListener(_viewModel.InvokeContinue);
            _viewModel.OnOpen -= InvokeOpen;
        }

        private void InvokeOpen(int hits, string score)
        {
            _settings.ScoreCountText.text = score;
            _settings.HitsViewer.SetPanelActive(true);
            _settings.HitsViewer.SetHits(hits);

        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public TMP_Text ScoreCountText { get; private set; }
            [field: SerializeField] public HitsViewer HitsViewer { get; private set; }
            [field: SerializeField] public Button ExitButton { get; set; }
        }
    }
}