using System;

namespace Game.Logic.Enemy.Ice.IceM
{
    public class IceMDamageHandler : EnemyDamageHandler
    {
        public IceMDamageHandler(IceSettings stats) : base(stats)
        {
        }

        [Serializable]
        public class IceSettings : EnemySettings { }
    }
}