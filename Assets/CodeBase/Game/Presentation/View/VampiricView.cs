using Core.MVVM.View;
using Game.Domain.Dto;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class VampiricView : AbstractPayloadView<VampiricViewModel>
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
        protected override void Construct(VampiricViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.OnUpdate += InvokeUpdate;
            gameObject.SetActive(false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _viewModel.OnUpdate -= InvokeUpdate;
        }

        private void InvokeUpdate(VampiricDto dto)
        {
            _settings.LoadFill.fillAmount = dto.Progress;
            _settings.LoadFill.gameObject.SetActive(dto.ShowLoad);
            _settings.Ready.SetActive(dto.ShowReady);
            _settings.Reload.SetActive(!dto.ShowReady);
            transform.SetSiblingIndex(dto.Order);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Image LoadFill { get; private set; }
            [field: SerializeField] public GameObject Ready { get; private set; }
            [field: SerializeField] public GameObject Reload { get; private set; }
        }
    }
}