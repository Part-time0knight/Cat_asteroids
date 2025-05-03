using System;
using UnityEngine;
using Zenject;

public class GameplayWindowInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        Container.BindMemoryPool<ScoreAnimation, ScoresView.Pool>()
                .FromComponentInNewPrefab(_settings.ScorePrefab).UnderTransform(_settings.Container);
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public ScoreAnimation ScorePrefab;
        [field: SerializeField] public RectTransform Container;
    }
}