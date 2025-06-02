using Game.Domain.Dto;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.Elements
{
    public class Bundle : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        private void Initialize(BundleDto bundleDto)
        {
            _settings.BuyButton.onClick.AddListener(() => bundleDto.OnBuy.Invoke());
            _settings.Cost.text = bundleDto.Cost;

            _settings.PlayerMutator.Name.text = bundleDto.TopName;
            _settings.PlayerMutator.Description.text = bundleDto.TopDescription;
            _settings.PlayerMutator.Icon.sprite = bundleDto.TopIcon;

            _settings.EnemyMutator.Name.text = bundleDto.BottomName;
            _settings.EnemyMutator.Description.text = bundleDto.BottomDescription;
            _settings.EnemyMutator.Icon.sprite = bundleDto.BottomIcon;
        }

        private void Clear()
        {
            _settings.BuyButton.onClick.RemoveAllListeners();
        }

        public class Pool : MonoMemoryPool<BundleDto, Bundle>
        {
            protected override void Reinitialize(BundleDto p1, Bundle item)
            {
                base.Reinitialize(p1, item);
                item.Initialize(p1);
            }

            protected override void OnDespawned(Bundle item)
            {
                base.OnDespawned(item);
                Clear();
            }
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button BuyButton { get; private set; }

            [field: SerializeField] public Mutator PlayerMutator { get; private set; }
            [field: SerializeField] public Mutator EnemyMutator { get; private set; }

            [field: SerializeField] public TMP_Text Cost {  get; private set; }

            [Serializable]
            public class Mutator
            {
                [field: SerializeField] public TMP_Text Name { get; private set; }
                [field: SerializeField] public TMP_Text Description { get; private set; }
                [field: SerializeField] public Image Icon { get; private set; }
            }
        }
    }
}