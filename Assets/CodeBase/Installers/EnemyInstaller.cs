using Game.Logic.Enemy;
using Game.Logic.Enemy.Fsm;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        InstallFactories();
        InstallFsm();

        InstallEnemyComponents();

    }

    private void InstallEnemyComponents()
    {
        Container.BindInstance(_settings.Body).AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyWeaponHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyMoveHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyTickHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyDamageHandler>().AsSingle();
    }

    private void InstallFsm()
    {
        Container
            .BindInterfacesTo<EnemyWindowFsm>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<EnemyFsm>()
            .AsSingle()
            .NonLazy();
    }

    private void InstallFactories()
    {
        Container
            .BindInterfacesAndSelfTo<EnemyStatesFactory>()
            .AsSingle()
            .NonLazy();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public Rigidbody2D Body { get; private set; }
    }
}