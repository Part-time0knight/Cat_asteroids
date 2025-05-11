using System;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.AsteroidM
{
    public class AsteroidMMoveHandler : EnemyMoveHandler
    {
        public AsteroidMMoveHandler(Rigidbody2D body, AsteroidSettings stats) : base(body, stats)
        {
        }

        [Serializable]
        public class AsteroidSettings : EnemySettings
        { 
        }
    }
}