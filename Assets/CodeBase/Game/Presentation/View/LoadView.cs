using Core.MVVM.View;
using DG.Tweening;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class LoadView : AbstractPayloadView<LoadViewModel>
    {
        [SerializeField] private Settings _settings;

        [Inject]
        protected override void Construct(LoadViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.OnLoad += UpdateLoad;
        }

        private void UpdateLoad(float progress)
        {
            _settings.FillLoad.DOKill();
            _settings.FillLoad.DOFillAmount(progress, _settings.FillAnimationSpeed);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _settings.FillLoad?.DOKill();
            _viewModel.OnLoad -= UpdateLoad;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Image FillLoad;
            [field: SerializeField] public float FillAnimationSpeed;
        }
    }
}