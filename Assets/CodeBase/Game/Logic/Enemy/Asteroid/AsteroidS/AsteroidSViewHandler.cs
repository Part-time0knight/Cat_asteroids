using System;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.AsteroidS
{
    public class AsteroidSViewHandler : AsteroidViewHandler
    {
        public AsteroidSViewHandler(SpriteRenderer spriteRenderer, AsteroidSSettings settings)
            : base(spriteRenderer, settings)
        {
        }

        [Serializable]
        public class AsteroidSSettings : Settings { }
    }
}