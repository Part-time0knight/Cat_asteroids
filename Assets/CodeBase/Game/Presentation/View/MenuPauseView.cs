using Core.MVVM.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class MenuPauseView : AbstractPayloadView<MenuPauseViewModel>
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _settingsButton;

        [Inject]
        protected override void Construct(MenuPauseViewModel viewModel)
        {
            base.Construct(viewModel);
            _backButton.onClick.AddListener(_viewModel.InvokeClose);
            _settingsButton.onClick.AddListener(_viewModel.OpenSettings);
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(_viewModel.InvokeClose);
            _settingsButton.onClick.RemoveListener(_viewModel.OpenSettings);
        }
    }
}