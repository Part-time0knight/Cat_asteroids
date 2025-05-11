using System;
using UnityEngine;

namespace Game.Logic.Enemy.Asteroid.AsteroidB
{
    public class AsteroidBRotate : AsteroidRotate
    {
        public AsteroidBRotate(SpriteRenderer sprite, AsteroidBSettings settings)
            : base(sprite, settings)
        {
        }

        [Serializable]
        public class AsteroidBSettings : Settings { }
    }
}