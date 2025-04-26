using Core.MVVM.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsView : AbstractPayloadView<SettingsViewModel>
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Toggle _soundToggle;

    [Inject]
    protected override void Construct(SettingsViewModel viewModel)
    {
        base.Construct(viewModel);
        _backButton.onClick.AddListener(_viewModel.InvokeClose);
        _soundToggle.onValueChanged.AddListener(_viewModel.ChangeSound);
        _viewModel.InvokeUpdate += ViewUpdate;
    }

    private void ViewUpdate(bool active)
    {
        _soundToggle.isOn = active;
    }
}
