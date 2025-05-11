using System;
using Zenject;

namespace Game.Logic.Enemy.Asteroid.AsteroidS
{
    public class AsteroidSmallHandler : EnemyHandler
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