using Core.MVVM.View;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class ControlWindowView : AbstractPayloadView<ControlWindowViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(ControlWindowViewModel viewModel)
        {
            base.Construct(viewModel);
            _settings.ExitButton.onClick.AddListener(_viewModel.InvokeClose);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _settings.ExitButton.onClick.RemoveListener(_viewModel.InvokeClose);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button ExitButton { get; set; }
        }
    }
}