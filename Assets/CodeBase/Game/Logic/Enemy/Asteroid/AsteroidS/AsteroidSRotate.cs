using System;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.AsteroidS
{
    public class AsteroidSRotate : AsteroidRotate
    {
        public AsteroidSRotate(SpriteRenderer sprite, AsteroidSSettings settings)
                : base(sprite, settings)
        {
        }

        [Serializable]
        public class AsteroidSSettings : Settings { }
    }
}