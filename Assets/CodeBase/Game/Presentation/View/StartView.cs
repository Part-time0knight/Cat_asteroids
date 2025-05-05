using Core.MVVM.View;
using DG.Tweening;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using Zenject;

namespace Game.Presentation.View
{
    public class StartView : AbstractPayloadView<StartViewModel>
    {
        [SerializeField] private Settings _settings;


        private float _baseAlpha = 0;
        private Sequence _sequence;


        [Inject]
        protected override void Construct(StartViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.OnOpen += InvokeOpen;
            _viewModel.OnClose += InvokeClose;
            _baseAlpha = _settings.CanvasGroup.alpha;
        }

        private void InvokeOpen()
        {
            _settings.CanvasGroup.alpha = 0;
            _sequence = DOTween.Sequence();
            _sequence
                .Append(_settings.CanvasGroup
                    .DOFade(0, _settings.FadeDuration)
                    .SetEase(Ease.OutQuad))
                .Append(_settings.CanvasGroup
                    .DOFade(_baseAlpha, _settings.FadeDuration)
                    .SetEase(Ease.OutQuad))
                .SetLoops(-1).Play();
        }

        private void InvokeClose()
        {
            _sequence.Kill();
        }

        private void OnDestroy()
        {
            _viewModel.OnOpen -= InvokeOpen;
            _viewModel.OnClose -= InvokeClose;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
            [field: SerializeField] public float FadeDuration { get; private set; }
        }
    }
}