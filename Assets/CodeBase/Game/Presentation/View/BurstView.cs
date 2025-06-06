using Core.MVVM.View;
using DG.Tweening;
using Game.Domain.Dto;
using Game.Presentation.ViewModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class BurstView : AbstractPayloadView<BurstViewModel>
    {
        [SerializeField] private Image _reloadFill;

        private Pool _pool;

        private List<Image> _ammoIcons = new();

        [Inject]
        private void Construct(BurstViewModel viewModel, Pool pool)
        {
            Construct(viewModel);
            _pool = pool;
            _viewModel.OnAmmoUpdate += SetAmmo;
            _viewModel.OnReloadUpdate += ReloadAnimation;
        }

        private void SetAmmo(int count)
        {
            ClearAmmo();
            for (int i = 0; i < count; i++)
                _ammoIcons.Add(_pool.Spawn());
        }

        private void ClearAmmo()
        {
            foreach (var item in _ammoIcons)
                _pool.Despawn(item);
            _ammoIcons.Clear();
        }

        private void ReloadAnimation(float time)
        {
            ClearAnimation();
            _reloadFill.DOFillAmount(1f, time);
        }

        private void ClearAnimation()
        {
            _reloadFill.DOKill();
            _reloadFill.fillAmount = 0;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ClearAnimation();

            _viewModel.OnAmmoUpdate -= SetAmmo;
            _viewModel.OnReloadUpdate -= ReloadAnimation;
        }

        public class Pool : MonoMemoryPool<Image>
        {

        }
    }
}