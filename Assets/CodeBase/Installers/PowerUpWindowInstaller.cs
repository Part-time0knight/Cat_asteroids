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
    }

    [Serializable]
    public class Settings
    {

        [field: SerializeField] public HitsViewerSettings HitsViewer;

        [Serializable]
        public class HitsViewerSettings
        {
            [field: SerializeField] public Image Prefab;
            [field: SerializeField] public RectTransform Container;
        }
    }
}