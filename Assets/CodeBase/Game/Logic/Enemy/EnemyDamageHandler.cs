using Game.Logic.Handlers;
using System;

namespace Game.Logic.Enemy
{
    public class EnemyDamageHandler : DamageHandler
    {
        public EnemyDamageHandler(EnemySettingsHandler stats, IPauseHandler pauseHandler) : base(stats.DamageSettings, pauseHandler)
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