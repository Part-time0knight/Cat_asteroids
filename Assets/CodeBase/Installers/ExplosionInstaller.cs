using System;
using UnityEngine;
using Zenject;

public class ExplosionInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        Container
            .BindInstance(_settings.ExplosionAnimator)
            .AsSingle();
        Container
            .BindInstance(_settings.ParticleSystem)
            .AsSingle();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public Animator ExplosionAnimator { get; private set; }
        [field: SerializeField] public ParticleSystem ParticleSystem { get; private set; }
    }
}