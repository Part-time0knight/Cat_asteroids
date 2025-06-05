using Game.Domain.Dto;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.Elements
{
    public class BundleMini : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        private void Initialize(BundleDtoMini dto)
        {
            _settings.ActiveButton.onClick.RemoveAllListeners();
            _settings.ActiveButton.onClick.AddListener(() => dto.OnClick.Invoke());

            _settings.PlayerMutatorIcon.sprite = dto.TopIcon;
            _settings.EnemyMutatorIcon.sprite = dto.BottomIcon;

            _settings.PlayerMutatorIcon.gameObject.SetActive(dto.TopActive);
            _settings.EnemyMutatorIcon.gameObject.SetActive(dto.BottomActive);
        }

        public class Pool : MonoMemoryPool<BundleDtoMini, BundleMini>
        {
            protected override void Reinitialize(BundleDtoMini p1, BundleMini item)
            {
                base.Reinitialize(p1, item);
                item.Initialize(p1);
            }

            protected override void OnDespawned(BundleMini item)
            {
                base.OnDespawned(item);
                Clear();
            }
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Button ActiveButton { get; private set; }

            [field: SerializeField] public Image PlayerMutatorIcon { get; private set; }
            [field: SerializeField] public Image EnemyMutatorIcon { get; private set; }

        }
    }
}