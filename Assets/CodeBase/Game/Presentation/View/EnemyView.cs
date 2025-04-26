using Core.MVVM.View;
using Game.Presentation.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class EnemyView : AbstractPayloadView<EnemyViewModel>
    {
        [SerializeField] private Image _hitsBarImage;

        [Inject]
        protected override void Construct(EnemyViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.InvokeHitsUpdate += HitsUpdate;
            _hitsBarImage.fillAmount = 1;
        }

        private void HitsUpdate(float ratioHits)
        {
            _hitsBarImage.fillAmount = ratioHits;
        }
    }
}