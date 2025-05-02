using Game.Logic.Player;
using Zenject;
using UnityEngine;
using System;
using Game.Logic.Player.Fsm;
using Game.Presentation.ViewModel;
using Game.Logic.Effects.Particles;
using Game.Logic.Player.Animation;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            InstallFactories();
            InstallPlayerComponents();
            //InstallViewModels();
            InstallFsm();

        }

        private void InstallFsm()
        {
            Container
                .BindInterfacesTo<PlayerWindowFsm>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<PlayerFsm>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallViewModels()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerViewModel>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallFactories()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerStatesFactory>()
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
                BindInterfacesAndSelfTo<PlayerInput>()
                .AsSingle();
            Container.
                BindInterfacesAndSelfTo<PlayerShootHandler>()
                .AsSingle();
            Container.
                BindInterfacesAndSelfTo<PlayerMoveHandler>()
                .AsSingle();
            Container.
                BindInterfacesAndSelfTo<PlayerDamageHandler>()
                .AsSingle();
            Container.
                BindInterfacesAndSelfTo<PlayerWeaponHandler>()
                .AsSingle();

            Container.
                BindInterfacesAndSelfTo<PlayerHasteEffectHandler>()
                .AsSingle()
                .WithArguments(_settings.HasteParticles);

            Container.
                BindInterfacesAndSelfTo<PlayerTakeDamage>()
                .AsSingle()
                .WithArguments(_settings.DamageParticles);

            Container.
                BindInterfacesAndSelfTo<PlayerInvincibilityHandler>()
                .AsSingle();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Transform Weapon { get; private set; }
            [field: SerializeField] public Rigidbody2D Body { get; private set; }
            [field: SerializeField] public ParticleSystem HasteParticles { get; private set; }
            [field: SerializeField] public ParticleSystem DamageParticles { get; private set; }
            [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        }
    }
}