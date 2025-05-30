using Game.Logic.Handlers;
using Game.Logic.Player.Animation;
using System;

namespace Game.Logic.Player.Handlers
{
    public class PlayerDamageHandler : DamageHandler, IPlayerDamageHandler
    {
        public bool Power { get; set; } = false;

        private readonly IPlayerHitsWriter _playerHitsWriter;
        private readonly PlayerTakeDamage _playerTakeDamage;

        public PlayerDamageHandler(IPlayerHitsWriter playerHitsWriter,
            PlayerTakeDamage playerTakeDamage,
            PlayerSettings stats) : base(stats)
        {
            _playerHitsWriter = playerHitsWriter;
            _playerTakeDamage = playerTakeDamage;
        }

        public override void Reset()
        {
            Power = false;
            _hits = _stats.HitPoints;
            _playerHitsWriter.Hits = _hits;
        }

        public override void TakeDamage(int damage)
        {
            if (Power)
                return;
            base.TakeDamage(damage);
            _playerHitsWriter.IsTakeDamage = true;
            _playerHitsWriter.Hits = _hits;
            _playerTakeDamage.Play();
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
        { }
    }
}