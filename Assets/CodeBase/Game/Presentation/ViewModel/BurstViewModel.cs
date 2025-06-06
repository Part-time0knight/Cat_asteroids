using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Domain.Dto;
using Game.Logic.Player;
using Game.Presentation.View;
using System;
using Zenject;

namespace Game.Presentation.ViewModel
{
    public class BurstViewModel : AbstractViewModel
    {
        public event Action<int> OnAmmoUpdate;
        public event Action<float> OnReloadUpdate;

        private readonly IBurstReader _burstData;

        protected override Type Window => typeof(BurstView);

        public BurstViewModel(IWindowFsm windowFsm,
            IBurstReader reader) : base(windowFsm)
        {
            _burstData = reader;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow(Window);
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: false);
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            if (uiWindow != Window) return;
            _burstData.OnAmmoChange += UpdateAmmo;
            _burstData.OnTimeChange += UpdateReload;
            UpdateAmmo();
            UpdateReload();
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            if (uiWindow != Window) return;
            _burstData.OnAmmoChange -= UpdateAmmo;
            _burstData.OnTimeChange -= UpdateReload;
        }

        private void UpdateAmmo()
        {
            OnAmmoUpdate?.Invoke(_burstData.Ammo);
        }

        private void UpdateReload()
        {
            OnReloadUpdate?.Invoke(_burstData.ReloadTime);
        }
    }
}