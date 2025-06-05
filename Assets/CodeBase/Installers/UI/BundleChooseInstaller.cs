using Game.Presentation.Elements;
using System;
using UnityEngine;
using Zenject;

namespace Installers.UI
{
    public class BundleChooseInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<BundleMini, BundleMini.Pool>()
                .FromComponentInNewPrefab(_settings.Prefab)
                .UnderTransform(_settings.Container);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public BundleMini Prefab;
            [field: SerializeField] public RectTransform Container;
        }
    }
}