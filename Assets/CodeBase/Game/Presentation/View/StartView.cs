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
            _settings.CanvasGroup.alpha = _settings.MinimalAlpha;
            _sequence = DOTween.Sequence();
            _sequence
                .Append(_settings.CanvasGroup
                    .DOFade(_baseAlpha, _settings.FadeDuration)
                    .SetEase(Ease.Linear))
                .Append(_settings.CanvasGroup
                    .DOFade(_settings.MinimalAlpha, _settings.FadeDuration)
                    .SetEase(Ease.Linear))
                .SetLoops(-1, LoopType.Restart).Play();
        }

        private void InvokeClose()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
            }
        }

        protected override void OnDestroy()
        {
            _viewModel.OnOpen -= InvokeOpen;
            _viewModel.OnClose -= InvokeClose;
            base.OnDestroy();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
            [field: SerializeField] public float FadeDuration { get; private set; }
            [field: SerializeField] public float MinimalAlpha { get; private set; }
        }
    }
}