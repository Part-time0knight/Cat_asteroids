using Game.Logic.Misc;
using Game.Logic.Player;
using System;
using UnityEngine;

namespace Game.Logic.Enemy
{
    public class EnemyWeaponHandler
    {
        private readonly Settings _settings;
        private readonly Timer _timer = new();
        private readonly PlayerHandler _playerHandler;

        public EnemyWeaponHandler(EnemySettingsHandler settings, PlayerHandler playerHandler)
        {
            _settings = settings.WeaponSettings;
            _settings.CurrentDamage = _settings.Damage;
            _playerHandler = playerHandler;
        }

        public void TickableDamage()
        {
            if (_timer.Active)
                return;
            _playerHandler.MakeCollizion(_settings.CurrentDamage);
            _timer.Initialize(_settings.DamageDelay).Play();
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