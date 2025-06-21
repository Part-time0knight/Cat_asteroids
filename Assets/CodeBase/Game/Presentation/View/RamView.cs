using Core.MVVM.View;
using DG.Tweening;
using Game.Domain.Dto;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class RamView : AbstractPayloadView<RamViewModel>
    {
        [SerializeField] private Settings _settings;

        public override void Show()
        {

            gameObject.SetActive(true);
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            gameObject.SetActive(false);
        }

        [Inject]
        protected override void Construct(RamViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.OnUpdate += UpdateView;
            _viewModel.OnPause += InvokePause;
            gameObject.SetActive(false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _viewModel.OnUpdate -= UpdateView;
            _viewModel.OnPause -= InvokePause;
        }

        private void UpdateView(RamDto dto)
        {
            Clear();
            _settings.LoadFill.gameObject.SetActive(dto.ShowLoad);
            _settings.ReadyText.SetActive(dto.ShowReady);
            transform.SetSiblingIndex(dto.Order);
            if (dto.ShowLoad)
                _settings.LoadFill.DOFillAmount(0, dto.LoadDuration);

        }

        private void InvokePause(bool pause)
        {
            if (!_settings.LoadFill.gameObject.activeSelf) return;

            if (pause)
                _settings.LoadFill.DOPause();
            else
            {
                _settings.LoadFill.fillAmount = 1f;
                _settings.LoadFill.DOPlay();
            }
        }


        private void Clear()
        {
            _settings.LoadFill.DOKill();
            _settings.LoadFill.fillAmount = 1;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Image LoadFill { get; private set; }

            [field: SerializeField] public GameObject ReadyText { get; private set; }
        }
    }
}