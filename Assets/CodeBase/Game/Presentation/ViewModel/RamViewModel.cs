using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Domain.Dto;
using Game.Logic.Player.Mutators.RamMutator;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class RamViewModel : AbstractViewModel
    {
        public event Action<RamDto> OnUpdate;
        public event Action<bool> OnPause;

        private readonly IRamReader _reader;
        private readonly RamDto _dto = new();
        private readonly BundleService _bundleService;

        protected override Type Window => typeof(RamView);

        public RamViewModel(IWindowFsm windowFsm,
            IRamReader reader,
            BundleService bundleService) : base(windowFsm)
        {
            _reader = reader;
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
            _reader.OnActivate += InvokeUpdate;
            _reader.OnReloadEnd += InvokeUpdate;
            _reader.OnPause += InvokePause;

            _dto.Order =
                _bundleService.GetSlotIndex((int)Mutator.Ram);

            InvokeUpdate();
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            if (uiWindow != Window) return;
            _reader.OnActivate -= InvokeUpdate;
            _reader.OnReloadEnd -= InvokeUpdate;
            _reader.OnPause -= InvokePause;
        }

        private void InvokeUpdate()
        {
            _dto.LoadDuration = _reader.ReloadTime;
            _dto.ShowLoad = _reader.Reload;
            _dto.ShowReady = !_reader.Reload;
            OnUpdate?.Invoke(_dto);
        }

        private void InvokePause(bool pause)
        {
            OnPause?.Invoke(pause);
        }
    }
}