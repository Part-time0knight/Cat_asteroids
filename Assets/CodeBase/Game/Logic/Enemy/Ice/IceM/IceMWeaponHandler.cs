using System;

namespace Game.Logic.Enemy.Ice.IceM
{
    public class IceMWeaponHandler : EnemyWeaponHandler
    {
        public IceMWeaponHandler(IceSettings settings) : base(settings)
        {
        }

        [Serializable]
        public class IceSettings : EnemySettings { }
    }
}