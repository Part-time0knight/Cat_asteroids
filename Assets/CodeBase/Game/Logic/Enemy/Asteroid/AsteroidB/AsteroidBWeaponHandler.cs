using System;

namespace Game.Logic.Enemy.Asteroid.AsteroidB
{
    public class AsteroidBWeaponHandler : EnemyWeaponHandler
    {
        public AsteroidBWeaponHandler(AsteroidSettings settings) : base(settings)
        {
        }

        [Serializable]
        public class AsteroidSettings : EnemySettings { }
    }
}