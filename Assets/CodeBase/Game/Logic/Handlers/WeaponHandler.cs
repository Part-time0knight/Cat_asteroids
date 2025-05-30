using Game.Logic.Misc;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Handlers
{
    public class WeaponHandler : IWeaponHandler
    {
        protected readonly Settings _settings;
        protected readonly Timer _reloadTimer = new();
        protected readonly Timer _delayTimer = new();

        protected UnitFacade _target;

        public WeaponHandler(Settings settings)
        {
            _settings = settings;
        }

        public virtual void FrameDamage(UnitFacade target)
        {
            if (_reloadTimer.Active)
                return;
            _target = target;
            DamageWithFrameDelay();
        }

        private void DamageWithFrameDelay()
        {
            if (_delayTimer.Active)
                return;
            _delayTimer.Initialize(Time.fixedDeltaTime, Time.fixedDeltaTime, MakeDamage).Play();
        }

        protected virtual void MakeDamage()
        {
            _target.MakeCollision(_settings.Damage);
            _reloadTimer.Initialize(_settings.DamageDelay).Play();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public int Damage { get; private set; }
            [field: SerializeField] public float DamageDelay { get; private set; }

        }
    }
}