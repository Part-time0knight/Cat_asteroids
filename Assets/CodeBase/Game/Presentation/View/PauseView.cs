using Core.MVVM.View;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class PauseView : AbstractPayloadView<PauseViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(PauseViewModel viewModel)
        {
            base.Construct(viewModel);
            _settings.Return.onClick.AddListener(_viewModel.InvokeReturn);
            _settings.GoToMenu.onClick.AddListener(_viewModel.InvokeGoToMainMenu);
            _settings.GoToControl.onClick.AddListener(_viewModel.InvokeGoToControl);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _settings.Return.onClick.RemoveListener(_viewModel.InvokeReturn);
            _settings.GoToMenu.onClick.RemoveListener(_viewModel.InvokeGoToMainMenu);
            _settings.GoToControl.onClick.RemoveListener(_viewModel.InvokeGoToControl);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button Return { get; private set; }
            [field: SerializeField] public Button GoToMenu { get; private set; }
            [field: SerializeField] public Button GoToControl { get; private set; }
        }
    }
}