using Game.Presentation.View;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Installers.UI
{
    public class BurstInstaller : MonoInstaller
    {
        [SerializeField] private PoolSettings _settings;

        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<Image, BurstView.Pool>()
                .FromComponentInNewPrefab(_settings.IconPrefab)
                .UnderTransform(_settings.Container);
        }

        [Serializable]
        public class PoolSettings
        {
            [field: SerializeField] public RectTransform Container { get; private set; }
            [field: SerializeField] public Image IconPrefab { get; private set; }
        }
    }
}