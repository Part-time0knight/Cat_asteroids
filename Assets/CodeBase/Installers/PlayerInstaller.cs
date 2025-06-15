using Zenject;
using UnityEngine;
using System;
using Game.Logic.Player.Fsm;
using Game.Logic.Effects.Particles;
using Game.Logic.Player.Animation;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Handlers.Factory;
using Game.Logic.Handlers;
using Game.Logic.Player.Mutators;
using Game.Logic.Player.Mutators.ProjectileMutators;
using Game.Logic.Player.Mutators.ShooterMutators;
using Game.Logic.Player.Mutators.TargetMutators;
using Game.Logic.Player.Handlers;
using Game.Logic.Player.Mutators.TargetMutator;
using Game.Logic.Player.Mutators.GyperjumpMutator;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            InstallFactories();
            InstallPlayerComponents();
            InstallFsm();
            InstallMutators();
        }

        private void InstallMutators()
        {
            Container
                .BindInterfacesAndSelfTo<BaseMutator>()
                .AsSingle();


            Container
                .BindInterfacesAndSelfTo<BaseProjectile>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<Laser>()
                .AsSingle();


            Container
                .BindInterfacesAndSelfTo<BaseShooter>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<Burst>()
                .AsSingle();

            Container.
                BindInterfacesAndSelfTo<BaseTarget>()
                .AsSingle();
            Container.
                BindInterfacesAndSelfTo<Aim>()
                .AsSingle();

            Container.
                BindInterfacesAndSelfTo<Gyperjump>()
                .AsSingle();
        }

        private void InstallFsm()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerFsm>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallFactories()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerStatesFactory>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<HandlerFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallPlayerComponents()
        {
            Container
                .BindInstance(_settings.Weapon)
                .AsSingle();
            Container
                .BindInstance(_settings.Body)
                .AsSingle();
            Container
                .BindInstance(_settings.SpriteRenderer)
                .AsSingle();

            Container.
                BindInterfacesAndSelfTo<HandlerStrategy>()
                .AsSingle();

            Container.
                BindInterfacesAndSelfTo<ProjectileManager>()
                .AsSingle();

            Container.
                BindInterfacesAndSelfTo<PlayerHasteEffect>()
                .AsSingle()
                .WithArguments(_settings.HasteParticles);

            Container.
                BindInterfacesAndSelfTo<PlayerTakeDamage>()
                .AsSingle()
                .WithArguments(_settings.DamageParticles);

            Container.
                BindInterfacesAndSelfTo<Invincibility>()
                .AsSingle()
                .WithArguments(_settings.InvincibilityRender);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Transform Weapon { get; private set; }
            [field: SerializeField] public Rigidbody2D Body { get; private set; }
            [field: SerializeField] public ParticleSystem HasteParticles { get; private set; }
            [field: SerializeField] public ParticleSystem DamageParticles { get; private set; }
            [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
            [field: SerializeField] public SpriteRenderer InvincibilityRender { get; private set; }
        }
    }
}