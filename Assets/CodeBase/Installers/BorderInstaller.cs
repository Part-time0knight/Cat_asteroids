using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BorderInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings _settings;

        public override void InstallBindings()
        {
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public List<Collider2D> Colliders;
        }
    }
}