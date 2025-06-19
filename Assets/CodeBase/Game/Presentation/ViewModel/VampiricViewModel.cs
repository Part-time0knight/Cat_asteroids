using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Domain.Dto;
using Game.Logic.Player.Mutators.VampiricMutator;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class VampiricViewModel : AbstractViewModel
    {
        public event Action<VampiricDto> OnUpdate;

        private readonly BundleService _bundleService;
        private readonly IVampiricReader _reader;
        private readonly VampiricDto _dto = new();
        

        protected override Type Window => typeof(VampiricView);

        public VampiricViewModel(IWindowFsm windowFsm, 
            IVampiricReader vampiricReader,
            BundleService bundleService) : base(windowFsm)
        {
            _reader = vampiricReader;
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

            _reader.OnUpdate += InvokeUpdate;

            _dto.Order =
                _bundleService.GetSlotIndex((int)Mutator.Vampiric);

            InvokeUpdate();
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            if (uiWindow != Window) return;

            _reader.OnUpdate -= InvokeUpdate;
        }

        private void InvokeUpdate()
        {
            _dto.ShowLoad = _reader.Reload;
            _dto.ShowReady = !_reader.Reload;

            _dto.Progress = (float)_reader.CurrentPoints / _reader.NeedPoints;

            OnUpdate?.Invoke(_dto);
        }
    }
}