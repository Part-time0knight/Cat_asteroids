using Game.Presentation.ViewModel;
using Core.MVVM.View;
using UnityEngine;
using System;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class DefeatView : AbstractPayloadView<DefeatViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(DefeatViewModel viewModel)
        {
            base.Construct(viewModel);
            _settings.Restart.onClick.AddListener(_viewModel.InvokeRestart);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _settings.Restart.onClick.RemoveListener(_viewModel.InvokeRestart);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button Restart { get; private set; }
        }
    }
}