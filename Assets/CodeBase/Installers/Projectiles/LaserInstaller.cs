using Game.Logic.Enemy.Ice;
using Game.Logic.Misc;
using System;
using UnityEngine;
using Zenject;

namespace Installers.Projectiles
{
    public class LaserInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            InstallComponent();
        }

        private void InstallComponent()
        {
            Container
                .BindInstance(_settings.Line)
                .AsSingle();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public LineRenderer Line { get; private set; }
        }
    }
}