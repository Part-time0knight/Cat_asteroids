using System;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.AsteroidM
{
    public class AsteroidMViewHandler : AsteroidViewHandler
    {
        public AsteroidMViewHandler(SpriteRenderer spriteRenderer, AsteroidMSettings settings) 
            : base(spriteRenderer, settings)
        {
        }

        [Serializable]
        public class AsteroidMSettings : Settings { }
    }
}