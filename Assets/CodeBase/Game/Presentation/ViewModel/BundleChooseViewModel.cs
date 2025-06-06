using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Domain.Dto;
using Game.Logic.Services.Mutators;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class BundleChooseViewModel : AbstractViewModel
    {

        public event Action<ShopMiniDto> OnOpen;

        private readonly BundleService _bundleService;

        private readonly ShopMiniDto _shopMiniDto = new();

        private readonly IMutatorData _mutatorData;

        protected override Type Window => typeof(BundleChooseView);

        public BundleChooseViewModel(IWindowFsm windowFsm,
            BundleService bundleService,
            IMutatorData mutatorData) : base(windowFsm)
        {
            _bundleService = bundleService;
            _mutatorData = mutatorData;
        }

        public void InitializeSlots()
        {
            var slots = _bundleService.Slots;
            _shopMiniDto.Bundles = new(slots.Count);
            for (int i = 0; i < slots.Count; i++)
            {
                int index = i;
                _shopMiniDto.Bundles.Add(new()
                {
                    TopActive = false,
                    BottomActive = false,

                    OnClick = () => InvokeChoose(index),
                });
            }
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow(Window);
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: false);
        }

        public void InvokeCancel()
        {
            InvokeClose();
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            if (Window != uiWindow) return;
            if (_shopMiniDto.Bundles.Count == 0)
                InitializeSlots();
            UpdateSlotList();
        }

        private void UpdateSlotList()
        {
            var slots = _bundleService.Slots;
            for (int i = 0; i < _shopMiniDto.Bundles.Count; i++)
            {
                _shopMiniDto.Bundles[i].TopActive = slots[i].PlayerId != -1;
                _shopMiniDto.Bundles[i].BottomActive = slots[i].EnemyId != -1;
                
                if (slots[i].PlayerId != -1)
                    _shopMiniDto.Bundles[i].TopIcon = _mutatorData.GetSprite(slots[i].PlayerId);

                if (slots[i].EnemyId != -1)
                    _shopMiniDto.Bundles[i].BottomIcon = _mutatorData.GetSprite(slots[i].EnemyId);
            }
            OnOpen?.Invoke(_shopMiniDto);
        }

        private void InvokeChoose(int ind)
        {
            _bundleService.SelectedSlot = ind;
            InvokeClose();
        }
    }
}