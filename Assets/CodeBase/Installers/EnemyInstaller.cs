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
        InstallViewModels();
        InstallFsm();

        InstallPlayerComponents();

    }

    private void InstallPlayerComponents()
    {
        Container.BindInstance(_settings.Body).AsSingle();
        Container.BindInstance(_settings.Animator).AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyWeaponHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyMoveHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyTickHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyDamageHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemySettingsHandler>().AsSingle();
    }

    private void InstallViewModels()
    {
        Container
            .BindInterfacesAndSelfTo<EnemyViewModel>()
            .AsSingle()
            .NonLazy();
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
        [field: SerializeField] public Animator Animator { get; private set; }
    }
}