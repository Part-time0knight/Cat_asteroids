using Core.MVVM.View;
using Game.Presentation.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class GameplayButtonsView : AbstractPayloadView<GameplayButtonsViewModel>
    {
        [SerializeField] private Button _menuPauseButton;
        [SerializeField] private Button _TestingToolsButton;

        [Inject]
        protected override void Construct(GameplayButtonsViewModel viewModel)
        {
            base.Construct(viewModel);
            _TestingToolsButton.onClick.AddListener(_viewModel.OpenTestingToolsWindow);
            _menuPauseButton.onClick.AddListener(_viewModel.OpenMenuPauseWindow);
        }

        private void OnDestroy()
        {
            _TestingToolsButton.onClick.RemoveListener(_viewModel.OpenTestingToolsWindow);
            _menuPauseButton.onClick.RemoveListener(_viewModel.OpenMenuPauseWindow);
        }
    }
}