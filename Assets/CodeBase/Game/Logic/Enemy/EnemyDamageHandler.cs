using Game.Logic.Handlers;
using System;

namespace Game.Logic.Enemy
{
    public class EnemyDamageHandler : DamageHandler
    {
        public EnemyDamageHandler(EnemySettings stats) : base(stats)
        { }

        public void Reset()
        {
            _hits = _stats.HitPoints;
        }

        [Serializable]
        public class EnemySettings : Settings
        { 
            public EnemySettings(Settings settings) : base(settings)
            { }
        }
    }
}