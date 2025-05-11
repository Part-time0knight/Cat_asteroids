using System;

namespace Game.Logic.Enemy.Asteroid.AsteroidB
{
    public class AsteroidBDamageHandler : EnemyDamageHandler
    {
        public AsteroidBDamageHandler(AsteroidSettings stats) : base(stats)
        {
        }

        [Serializable]
        public class AsteroidSettings : EnemySettings { }
    }
}