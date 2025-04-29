using Game.Logic.Handlers;
using Game.Logic.Misc;
using Game.Logic.Player;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game.Logic.Enemy
{
    public class EnemyWeaponHandler
    {
        private readonly Settings _settings;
        private readonly Timer _reloadTimer = new();
        private readonly Timer _delayTimer = new();

        private UnitHandler _target;

        public EnemyWeaponHandler(Settings settings)
        {
            _settings = settings;
            _settings.CurrentDamage = _settings.Damage;
        }

        public void TickableDamage(UnitHandler target)
        {
            if (_reloadTimer.Active)
                return;
            _target = target;
            DamageWithDelay();
        }

        private void DamageWithDelay()
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

            public Settings()
            { }

            public Settings(int damage, int currentDamage, float damageDelay)
            {
                Damage = damage;
                CurrentDamage = currentDamage;
                DamageDelay = damageDelay;
            }

            public Settings(Settings settings) : this(
                settings.Damage,
                settings.CurrentDamage,
                settings.DamageDelay)
            { }
        }
    }
}