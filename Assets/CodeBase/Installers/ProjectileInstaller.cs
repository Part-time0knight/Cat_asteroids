using Game.Logic.Misc;
using System;
using UnityEngine;
using Zenject;

public class ProjectileInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        Container.BindInstance(_settings.Body).AsSingle();
        InstallMoveHandler();
    }

    protected virtual void InstallMoveHandler()
    {
        Container.BindInterfacesAndSelfTo<BulletMoveHandler>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public Rigidbody2D Body { get; private set; }
    }
}
