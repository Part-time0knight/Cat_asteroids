using Game.Logic.Handlers;
using System;
using Zenject;

namespace Game.Logic.Projectiles
{
    public class ProjectileDamageHandler : DamageHandler
    {
        public ProjectileDamageHandler(ProjectileSettings stats) : base(stats)
        {

        }

        [Serializable]
        public class ProjectileSettings : Settings
        {
            
        }
    }
}