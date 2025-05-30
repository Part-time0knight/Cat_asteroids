using System;
using Zenject;

namespace Game.Logic.Enemy.Asteroid.AsteroidS
{
    public class AsteroidSmallHandler : EnemyFacade
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