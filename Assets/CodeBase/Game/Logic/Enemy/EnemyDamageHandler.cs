using Game.Logic.Handlers;
using System;

namespace Game.Logic.Enemy
{
    public class EnemyDamageHandler : DamageHandler
    {
        public EnemyDamageHandler(EnemySettingsHandler stats) : base(stats.DamageSettings)
        { }

        public void Reset()
        {
            _stats.CurrentHits = _stats.HitPoints;
        }

        [Serializable]
        public class EnemySettings : Settings
        { 
            public EnemySettings(Settings settings) : base(settings)
            { }
        }
    }
}