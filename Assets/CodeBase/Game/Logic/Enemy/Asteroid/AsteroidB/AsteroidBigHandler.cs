using System;
using Zenject;

namespace Game.Logic.Enemy.Asteroid.AsteroidB
{
    public class AsteroidBigHandler : EnemyHandler
    {
        [Inject]
        private void InjectSettings(AsteroidSettings settings)
        {
            SetSettings(settings);
        }

        [Serializable]
        public class AsteroidSettings : Settings { }
    }
}