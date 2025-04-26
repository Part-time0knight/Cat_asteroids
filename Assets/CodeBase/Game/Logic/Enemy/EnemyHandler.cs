using Game.Logic.Enemy.Fsm;
using Game.Logic.Enemy.Fsm.States;
using Game.Logic.Handlers;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy
{
    public class EnemyHandler : UnitHandler
    {
        public event Action<EnemyHandler> InvokeDeath;

        private EnemyDamageHandler _damageHandler;
        private EnemyDamageHandler.EnemySettings _damageSettings;
        private EnemyFsm _fsm;

        public override void MakeCollizion(int damage)
            => TakeDamage(damage);

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
            if (_damageSettings.CurrentHits == 0)
                InvokeDeath?.Invoke(this);
        }

        public Vector2 GetPosition()
            => transform.position;

        [Inject]
        private void Construct(EnemyDamageHandler damageHandler,
            EnemySettingsHandler damageSettings,
            EnemyFsm fsm)
        {
            _damageHandler = damageHandler;
            _damageSettings = damageSettings.DamageSettings;
            _fsm = fsm;
        }

        private void Initialize(Vector2 spawnPoint)
        {
            _damageHandler.Reset();
            _fsm.Enter<Run>();
            transform.position = spawnPoint;
        }

        public class Pool : MonoMemoryPool<Vector2, EnemyHandler>
        {
            protected override void Reinitialize(Vector2 spawnPoint, EnemyHandler item)
            {
                base.Reinitialize(spawnPoint, item);
                item.Initialize(spawnPoint);
            }
        }
    }
}