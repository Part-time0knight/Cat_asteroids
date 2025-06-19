using Game.Logic.Handlers;
using Game.Logic.Player.Animation;
using System;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class PlayerDamageHandler : DamageHandler, IPlayerDamageHandler
    {
        public event Action OnTryTakeDamage;

        public override event Action<int> OnTakeDamage;
        public override event Action OnDeath;

        private readonly IPlayerHitsWriter _playerHitsWriter;
        private readonly PlayerTakeDamage _playerTakeDamage;
        private readonly PlayerSettings _playerSettings;

        private int _shieldHits;

        public bool Power { get; set; } = false;

        public PlayerDamageHandler(IPlayerHitsWriter playerHitsWriter,
            PlayerTakeDamage playerTakeDamage,
            PlayerSettings stats) : base(stats)
        {
            _playerHitsWriter = playerHitsWriter;
            _playerTakeDamage = playerTakeDamage;
            _playerSettings = stats;
        }

        public override void Reset()
        {
            Power = false;
            _hits = _stats.HitPoints;
            _shieldHits = _playerSettings.ShieldHits;
            _playerHitsWriter.Hits = _hits;
            _playerHitsWriter.ShieldHits = _shieldHits;
        }

        public override void TakeDamage(int damage)
        {
            OnTryTakeDamage?.Invoke();

            if (Power)
                return;

            if (_shieldHits > 0)
            {
                int shield = _shieldHits;
                _shieldHits -= damage;
                _shieldHits = _shieldHits < 0 ? 0 : _shieldHits;
                damage -= shield;
                damage = damage < 0 ? 0 : damage;
            }

            _hits -= damage;
            _hits = Mathf.Max(Mathf.Min(_hits, _stats.HitPoints), 0);
            if (_hits <= 0)
                OnDeath?.Invoke();
            else
                OnTakeDamage?.Invoke(_hits);


            _playerHitsWriter.IsTakeDamage = true;
            _playerHitsWriter.Hits = _hits;
            _playerHitsWriter.ShieldHits = _shieldHits;
            _playerTakeDamage.Play();
        }

        public void SetShield(int hits)
        {
            _shieldHits += hits;
            _playerHitsWriter.IsTakeDamage = false;
            _playerHitsWriter.ShieldHits = _shieldHits;

        }

        public void Pause()
        {
            _playerTakeDamage.Pause();
        }

        public void Continue()
        {
            _playerTakeDamage.Continue();
        }


        [Serializable]
        public class PlayerSettings : Settings
        {
            [field: SerializeField] public int ShieldHits { get; private set; } = 0;
        }
    }
}