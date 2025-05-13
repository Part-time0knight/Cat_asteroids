using Core.MVVM.View;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class MainMenuView : AbstractPayloadView<MainMenuViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(MainMenuViewModel viewModel)
        {
            base.Construct(viewModel);
            _settings.StartButton.onClick.AddListener(_viewModel.StartGame);
        }

        protected override void OnDestroy()
        {
            _settings.StartButton.onClick.RemoveListener(_viewModel.StartGame);
            base.OnDestroy();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button StartButton { get; private set; }
        }
    }
}