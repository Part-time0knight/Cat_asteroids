using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player
{
    public class PlayerDamageHandler : DamageHandler
    {
        public bool Pause { get; set; }

        public PlayerDamageHandler(PlayerSettings stats) : base(stats)
        {
        }

        public override void TakeDamage(int damage)
        {
            if (!Pause)
                base.TakeDamage(damage);
        }


        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}