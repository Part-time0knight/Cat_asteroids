using Game.Logic.Enemy.Ice.Fsm.States;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy.Ice.IceM
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

        protected override void Initialize(Vector2 spawnPoint, Vector2 direction)
        {
            base.Initialize(spawnPoint, direction);
        }

        [Serializable]
        public class IceSettings : Settings { }
    }
}