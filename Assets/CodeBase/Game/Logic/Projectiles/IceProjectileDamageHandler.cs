
using System;

namespace Game.Logic.Projectiles
{
    public class IceProjectileDamageHandler : ProjectileDamageHandler
    {
        public IceProjectileDamageHandler(IceSettings stats) : base(stats)
        {
        }

        [Serializable]
        public class IceSettings : ProjectileSettings
        {

        }
    }
}