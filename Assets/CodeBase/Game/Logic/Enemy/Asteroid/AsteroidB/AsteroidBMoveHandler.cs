using System;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.AsteroidB
{
    public class AsteroidBMoveHandler : EnemyMoveHandler
    {
        public AsteroidBMoveHandler(Rigidbody2D body, AsteroidSettings stats) : base(body, stats)
        {
        }

        [Serializable]
        public class AsteroidSettings : EnemySettings
        {
        }
    }
}