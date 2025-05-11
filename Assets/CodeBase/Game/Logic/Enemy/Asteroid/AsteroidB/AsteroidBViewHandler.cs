using System;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.AsteroidB
{
    public class AsteroidBViewHandler : AsteroidViewHandler
    {
        public AsteroidBViewHandler(SpriteRenderer spriteRenderer, AsteroidBSettings settings)
            : base(spriteRenderer, settings)
        {
        }

        [Serializable]
        public class AsteroidBSettings : Settings { }
    }
}