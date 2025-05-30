using System;
using Zenject;

namespace Game.Logic.Enemy.Asteroid.AsteroidM
{
    public class AsteroidMediumHandler : EnemyFacade
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