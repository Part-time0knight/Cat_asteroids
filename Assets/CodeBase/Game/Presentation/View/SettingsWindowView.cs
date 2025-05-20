using Core.MVVM.View;
using Game.Presentation.ViewModel;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class SettingsWindowView : AbstractPayloadView<SettingsWindowViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(SettingsWindowViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.OnSoundChange += SoundUpdate;
            _viewModel.OnMusicChange += MusicUpdate;
            _settings.SoundBar.Scrollbar.onValueChanged.AddListener(InvokeSoundScrollChange);
            _settings.MusicBar.Scrollbar.onValueChanged.AddListener(InvokeMusicScrollChange);
            _settings.CloseButton.onClick.AddListener(_viewModel.InvokeClose);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _viewModel.OnSoundChange -= SoundUpdate;
            _viewModel.OnMusicChange -= MusicUpdate;
            _settings?.CloseButton.onClick.RemoveListener(_viewModel.InvokeClose);
        }

        private void SoundUpdate(float value, string valueView)
        {
            _settings.SoundBar.ValueText.text = valueView;
            _settings.SoundBar.Scrollbar.value = value;
        }

        private void MusicUpdate(float value, string valueView)
        {
            _settings.MusicBar.ValueText.text = valueView;
            _settings.MusicBar.Scrollbar.value = value;
        }

        private void InvokeSoundScrollChange(float value)
        {
            _viewModel.SetSoundVolume(value);
        }

        private void InvokeMusicScrollChange(float value)
        {
            _viewModel?.SetMusicVolume(value);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public SettingsScrollbar SoundBar { get; private set; }
            [field: SerializeField] public SettingsScrollbar MusicBar { get; private set; }

            [field: SerializeField] public Button CloseButton { get; private set; }

            [Serializable]
            public class SettingsScrollbar
            {
                [field: SerializeField] public TMP_Text ValueText { get; private set; }
                [field: SerializeField] public Scrollbar Scrollbar { get; private set; }
            }
        }
    }
}