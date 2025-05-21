using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Presentation.View;
using System;
using UnityEngine;
using Zenject;

namespace Game.Presentation.ViewModel
{
    public class SettingsWindowViewModel : AbstractViewModel, IInitializable
    {
        public event Action<float, string> OnSoundChange;
        public event Action<float, string> OnMusicChange;

        private float _soundVolume = 1f;
        private float _musicVolume = 1f;

        protected override Type Window => typeof(SettingsWindowView);

        public SettingsWindowViewModel(IWindowFsm windowFsm) : base(windowFsm)
        {
        }

        public void Initialize()
        {
            _soundVolume = 1f;
            _musicVolume = 1f;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        public void SetSoundVolume(float volume)
        {
            if (Mathf.Abs(_soundVolume - volume) < 0.001f) return;
            _soundVolume = volume;
            UpdateSoundVolumes();
        }

        public void SetMusicVolume(float volume)
        {
            if (Mathf.Abs(_musicVolume - volume) < 0.001f) return;
            _musicVolume = volume;
            UpdateMusicVolumes();
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            if (Window != uiWindow) return;
            UpdateSoundVolumes();
            UpdateMusicVolumes();
        }

        private void UpdateSoundVolumes()
        {
            OnSoundChange?.Invoke(_soundVolume, Mathf.Round(_soundVolume * 100).ToString("0.") + "%");
        }

        private void UpdateMusicVolumes()
        {
            OnMusicChange?.Invoke(_musicVolume, Mathf.Round(_musicVolume * 100).ToString("0.") + "%");
        }
    }
}