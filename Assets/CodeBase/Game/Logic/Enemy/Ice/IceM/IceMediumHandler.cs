using System;
using Zenject;

namespace Game.Logic.Enemy.Ice.IceM
{
    public class IceMediumHandler : EnemyHandler
    {
        [Inject]
        private void InjectSettings(IceSettings settings)
        {
            SetSettings(settings);
        }

        [Serializable]
        public class IceSettings : Settings { }
    }
}