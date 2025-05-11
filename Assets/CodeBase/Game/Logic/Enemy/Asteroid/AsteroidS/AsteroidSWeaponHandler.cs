using System;

namespace Game.Logic.Enemy.Asteroid.AsteroidS
{
    public class AsteroidSWeaponHandler : EnemyWeaponHandler
    {
        public AsteroidSWeaponHandler(AsteroidSettings settings) : base(settings)
        {
        }

        [Serializable]
        public class AsteroidSettings : EnemySettings { }
    }
}