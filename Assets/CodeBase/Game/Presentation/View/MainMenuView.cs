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
            _settings.AuthorButton.onClick.AddListener(_viewModel.OpenAuthorWindow);
            _settings.ControlButton.onClick.AddListener(_viewModel.OpenControlWindow);
            _settings.SettingsButton.onClick.AddListener(_viewModel.OpenSettingsWindow);
            _settings.LeaderBoardButton.onClick.AddListener(_viewModel.OpenLeaderBoardWindow);

        }

        protected override void OnDestroy()
        {
            _settings.StartButton.onClick.RemoveListener(_viewModel.StartGame);
            _settings.AuthorButton.onClick.RemoveListener(_viewModel.OpenAuthorWindow);
            _settings.ControlButton.onClick.RemoveListener(_viewModel.OpenControlWindow);
            _settings.SettingsButton.onClick.RemoveListener(_viewModel.OpenSettingsWindow);
            _settings.LeaderBoardButton.onClick.RemoveListener(_viewModel.OpenLeaderBoardWindow);
            base.OnDestroy();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button StartButton { get; private set; }
            [field: SerializeField] public Button ControlButton { get; private set;}
            [field: SerializeField] public Button SettingsButton { get; private set; }

            [field: SerializeField] public Button LeaderBoardButton { get; private set; }

            [field: SerializeField] public Button AuthorButton { get; private set; }
        }
    }
}