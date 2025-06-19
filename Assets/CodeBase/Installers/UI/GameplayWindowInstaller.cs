using Game.Presentation.Elements;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Installers.UI
{
    public class GameplayWindowInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<ScoreAnimation, ScoreViewer.Pool>()
                .FromComponentInNewPrefab(_settings.ScoreViewer.Prefab)
                .UnderTransform(_settings.ScoreViewer.Container);

            Container
                .BindMemoryPool<Image, HitsViewer.Pool>()
                .FromComponentInNewPrefab(_settings.HitsViewer.Prefab)
                .UnderTransform(_settings.HitsViewer.Container);

            Container
                .Bind<Sprite>()
                .WithId("Heart")
                .FromInstance(_settings.HitsViewer.Heart);

            Container
                .Bind<Sprite>()
                .WithId("Shield")
                .FromInstance(_settings.HitsViewer.Shield);
        }

        [Serializable]
        public class Settings
        {

            [field: SerializeField] public ScoreViewerSettings ScoreViewer;
            [field: SerializeField] public HitsViewerSettings HitsViewer;


            [Serializable]
            public class ScoreViewerSettings
            {
                [field: SerializeField] public ScoreAnimation Prefab;
                [field: SerializeField] public RectTransform Container;
            }

            [Serializable]
            public class HitsViewerSettings
            {
                [field: SerializeField] public Sprite Heart;
                [field: SerializeField] public Sprite Shield;
                [field: SerializeField] public Image Prefab;
                [field: SerializeField] public RectTransform Container;
            }
        }
    }
}