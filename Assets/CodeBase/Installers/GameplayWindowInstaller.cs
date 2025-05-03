using System;
using UnityEngine;
using Zenject;

public class GameplayWindowInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        Container
            .BindInstance(_settings.Container)
            .AsSingle();
        Container.BindMemoryPool<ScoreAnimation, ScoresView.Pool>()
                .FromComponentInNewPrefab(_settings.ScorePrefab);
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public ScoreAnimation ScorePrefab;
        [field: SerializeField] public RectTransform Container;
    }
}