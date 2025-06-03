using Core.MVVM.View;
using Game.Domain.Dto;
using Game.Presentation.Elements;
using Game.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class PowerUpView : AbstractPayloadView<PowerUpViewModel>
    {
        [SerializeField] private Settings _settings;
        
        private ParticleSystem _bigExplosion;
        private Bundle.Pool _bundlePool;
        private List<Bundle> _bundles = new();
        private string _title;

        [Inject]
        private void Construct(PowerUpViewModel viewModel,
            ParticleSystem bigExplosion,
            Bundle.Pool bundlePool)
        {
            Construct(viewModel);
            _settings
                .ExitButton
                .onClick
                .AddListener(_viewModel.InvokeContinue);
            _bigExplosion = bigExplosion;
            _viewModel.OnOpen += InvokeOpen;
            //_viewModel.OnShopUpdate += InvokeShopUpdate;
            _title = _settings.TitleText.text;
            _bundlePool = bundlePool;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _settings.ExitButton.onClick.RemoveListener(_viewModel.InvokeContinue);
            _viewModel.OnOpen -= InvokeOpen;
            //_viewModel.OnShopUpdate -= InvokeShopUpdate;
        }

        private void InvokeOpen(PowerUpDto dto)
        {
            _settings.TitleText.text = _title.Replace("{data}", dto.Layer);
            _settings.ScoreCountText.text = dto.Score;
            _settings.LayerStepCountText.text = dto.ScoreToNextLayer;
            _settings.HitsViewer.SetPanelActive(true);
            _settings.HitsViewer.SetHits(dto.Hits);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_settings.ScoreCountText.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_settings.LayerStepCountText.rectTransform);
            _bigExplosion.Play();
        }

        private void InvokeShopUpdate(ShopDto dto)
        {
            ClearBundles();
            foreach (var item in dto.Bundles)
            {
                _bundles.Add(_bundlePool.Spawn(item));
            }
        }

        private void ClearBundles()
        {
            foreach (var item in _bundles)
                if (item != null)
                    _bundlePool.Despawn(item);
            _bundles.Clear();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public TMP_Text TitleText { get; private set; }
            [field: SerializeField] public TMP_Text ScoreCountText { get; private set; }
            [field: SerializeField] public TMP_Text LayerStepCountText { get; private set; }
            [field: SerializeField] public HitsViewer HitsViewer { get; private set; }
            [field: SerializeField] public Button ExitButton { get; set; }
        }
    }
}