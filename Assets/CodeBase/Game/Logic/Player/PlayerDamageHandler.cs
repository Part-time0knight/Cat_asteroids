using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player
{
    public class PlayerDamageHandler : DamageHandler
    {
        public bool Pause { get; set; }

        private readonly IPlayerHitsWriter _playerHitsWriter;

        public PlayerDamageHandler(IPlayerHitsWriter playerHitsWriter, PlayerSettings stats) : base(stats)
        {
            _playerHitsWriter = playerHitsWriter;
            _playerHitsWriter.Hits = stats.HitPoints;
        }

        public override void TakeDamage(int damage)
        {
            if (Pause)
                return;
            base.TakeDamage(damage);
            _playerHitsWriter.Hits = _hits;
        }


        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}