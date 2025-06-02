using Game.Presentation.Elements;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PowerUpWindowInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        Container
            .BindMemoryPool<Image, HitsViewer.Pool>()
            .FromComponentInNewPrefab(_settings.HitsViewer.Prefab)
            .UnderTransform(_settings.HitsViewer.Container);

        Container
            .BindMemoryPool<Bundle, Bundle.Pool>()
            .FromComponentInNewPrefab(_settings.Bundle.Prefab)
            .UnderTransform(_settings.Bundle.Container);
    }

    [Serializable]
    public class Settings
    {

        [field: SerializeField] public HitsViewerSettings HitsViewer;
        [field: SerializeField] public BundleSettings Bundle;

        [Serializable]
        public class HitsViewerSettings
        {
            [field: SerializeField] public Image Prefab;
            [field: SerializeField] public RectTransform Container;
        }

        [Serializable]
        public class BundleSettings
        {
            [field: SerializeField] public Bundle Prefab;
            [field: SerializeField] public RectTransform Container;
        }
    }
}