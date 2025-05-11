using System;

namespace Game.Logic.Enemy.Asteroid.AsteroidM
{
    public class AsteroidMDamageHandler : EnemyDamageHandler
    {
        public AsteroidMDamageHandler(AsteroidSettings stats) : base(stats)
        {
        }

        [Serializable]
        public class AsteroidSettings : EnemySettings { }
    }
}