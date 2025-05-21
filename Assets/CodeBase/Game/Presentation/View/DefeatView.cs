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
            _settings.MainMenu.onClick.AddListener(_viewModel.InvokeMainMenu);
            _settings.LeaderBoard.onClick.AddListener(_viewModel.InvokeLeaderBoard);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _settings.Restart.onClick.RemoveListener(_viewModel.InvokeRestart);
            _settings.MainMenu.onClick.RemoveListener(_viewModel.InvokeMainMenu);
            _settings.LeaderBoard.onClick.RemoveListener(_viewModel.InvokeLeaderBoard);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button Restart { get; private set; }
            [field: SerializeField] public Button LeaderBoard { get; private set; }
            [field: SerializeField] public Button MainMenu { get; private set; }
        }
    }
}