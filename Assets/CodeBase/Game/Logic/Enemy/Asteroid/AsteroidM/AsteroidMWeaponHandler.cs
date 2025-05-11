using System;

namespace Game.Logic.Enemy.Asteroid.AsteroidM
{
    public class AsteroidMWeaponHandler : EnemyWeaponHandler
    {
        public AsteroidMWeaponHandler(AsteroidSettings settings) : base(settings)
        {
        }

        [Serializable]
        public class AsteroidSettings : EnemySettings { }
    }
}