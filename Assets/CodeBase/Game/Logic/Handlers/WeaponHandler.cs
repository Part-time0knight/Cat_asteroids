using Game.Logic.Misc;
using System;
using UnityEngine;

namespace Game.Logic.Handlers
{
    public class WeaponHandler
    {
        private readonly Settings _settings;
        private readonly Timer _reloadTimer = new();
        private readonly Timer _delayTimer = new();

        private UnitHandler _target;

        public WeaponHandler(Settings settings)
        {
            _settings = settings;
            _settings.CurrentDamage = _settings.Damage;
        }

        public void TickableDamage(UnitHandler target)
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
            _delayTimer.Initialize(0.01f, 0.01f, MakeDamage).Play();
        }

        private void MakeDamage()
        {
            _target.MakeCollizion(_settings.CurrentDamage);
            _reloadTimer.Initialize(_settings.DamageDelay).Play();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public int Damage { get; private set; }
            [field: SerializeField] public float DamageDelay { get; private set; }
            public int CurrentDamage { get; set; }

        }
    }
}