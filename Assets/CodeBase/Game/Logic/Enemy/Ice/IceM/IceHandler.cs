using Game.Logic.Enemy.Ice.Fsm.States;
using System;
using Zenject;

namespace Game.Logic.Enemy.Ice.IceM
{
    public class IceHandler : EnemyHandler
    {
        public override void Clear()
        {
            base.Clear();
            _fsm.Enter<Clear>();
        }

        [Inject]
        private void InjectSettings(IceSettings settings)
        {
            SetSettings(settings);
        }

        [Serializable]
        public class IceSettings : Settings { }
    }
}