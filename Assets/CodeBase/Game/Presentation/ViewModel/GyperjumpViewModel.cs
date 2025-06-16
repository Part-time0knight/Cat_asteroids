using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Domain.Dto;
using Game.Logic.Player.Mutators.GyperjumpMutator;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class GyperjumpViewModel : AbstractViewModel
    {
        public event Action<GyperjumpDto> OnUpdate;
        public event Action<bool> OnPause;

        private readonly IGyperjumpReader _reader;
        private readonly GyperjumpDto _dto = new();
        private readonly BundleService _bundleService;

        protected override Type Window => typeof(GyperjumpView);

        public GyperjumpViewModel(IWindowFsm windowFsm,
            IGyperjumpReader gyperjumpReader,
            BundleService bundleService) : base(windowFsm)
        {
            _reader = gyperjumpReader;
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
                _bundleService.GetSlotIdFromMutatorId((int)Mutator.Gyperjump);

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