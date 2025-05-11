using System;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.AsteroidM
{
    public class AsteroidMRotate : AsteroidRotate
    {
        public AsteroidMRotate(SpriteRenderer sprite, AsteroidMSettings settings) 
            : base(sprite, settings)
        {
        }

        [Serializable]
        public class AsteroidMSettings : Settings { }
    }
}