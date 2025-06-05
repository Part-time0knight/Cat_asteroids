using Core.MVVM.View;
using Game.Domain.Dto;
using Game.Presentation.Elements;
using Game.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class BundleChooseView : AbstractPayloadView<BundleChooseViewModel>
    {
        [SerializeField] private Settings _settings;

        private readonly List<BundleMini> _slots = new();
        private BundleMini.Pool _pool;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _viewModel.OnOpen -= InvokeShow;
            _settings.BackButton.onClick.RemoveListener(_viewModel.InvokeCancel);
        }

        [Inject]
        private void Construct(BundleChooseViewModel viewModel, BundleMini.Pool pool)
        {
            Construct(viewModel);
            _pool = pool;
            _viewModel.OnOpen += InvokeShow;
            _settings.BackButton.onClick.AddListener(_viewModel.InvokeCancel);
        }

        private void InvokeShow(ShopMiniDto dto)
        {
            Clear();
            foreach (var bundle in dto.Bundles) 
            {
                _slots.Add(_pool.Spawn(bundle));
            }
        }

        private void Clear()
        {
            foreach (var bundle in _slots)
                _pool.Despawn(bundle);

            _slots.Clear();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button BackButton { get; private set; }
        }
    }
}