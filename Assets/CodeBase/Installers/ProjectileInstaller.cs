using Game.Logic.Misc;
using Game.Logic.Projectiles;
using System;
using UnityEngine;
using Zenject;

public class ProjectileInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        Container.BindInstance(_settings.Body).AsSingle();
        InstallHandlers();
    }

    protected virtual void InstallHandlers()
    {
        Container
            .BindInterfacesAndSelfTo<BulletMoveHandler>()
            .AsSingle();
        
        Container
            .BindInterfacesAndSelfTo<ProjectileDamageHandler>()
            .AsSingle();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public Rigidbody2D Body { get; private set; }
    }
}
