using Game.Logic.Enemy.Ice.Fsm.States;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy.Ice
{
    public class IceFacade : EnemyFacade
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