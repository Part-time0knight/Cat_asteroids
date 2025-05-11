using System;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.AsteroidS
{
    public class AsteroidSDamageHandler : EnemyDamageHandler
    {
        public AsteroidSDamageHandler(AsteroidSettings stats) : base(stats)
        {
        }

        [Serializable]
        public class AsteroidSettings : EnemySettings { }
    }
}