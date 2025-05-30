using Game.Logic.Handlers;
using System;

namespace Game.Logic.Enemy
{
    public class EnemyDamageHandler : DamageHandler
    {
        public EnemyDamageHandler(EnemySettings stats) : base(stats)
        { }

        [Serializable]
        public class EnemySettings : Settings
        { 

        }
    }
}