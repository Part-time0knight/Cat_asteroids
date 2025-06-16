using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Logic.Player.Mutators.ShooterMutators;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class BurstViewModel : AbstractViewModel
    {
        public event Action<int> OnOrderUpdate;
        public event Action<int> OnAmmoUpdate;
        public event Action<float> OnReloadUpdate;

        private readonly IBurstReader _burstData;
        private readonly BundleService _bundleService;

        protected override Type Window => typeof(BurstView);

        public BurstViewModel(IWindowFsm windowFsm,
            IBurstReader reader,
            BundleService bundleService) : base(windowFsm)
        {
            _burstData = reader;
            _bundleService = bundleService;
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

            OnOrderUpdate?
                .Invoke(_bundleService.GetSlotIdFromMutatorId((int)Mutator.Burst));

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