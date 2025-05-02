using Game.Logic.Enemy.Fsm;
using Game.Logic.Enemy.Fsm.States;
using Game.Logic.Handlers;
using Game.Logic.Weapon;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy
{
    public class EnemyHandler : UnitHandler
    {
        public event Action<EnemyHandler> OnDeath;
        public event Action<EnemyHandler> OnDeactivate;

        private EnemyDamageHandler _damageHandler;
        private EnemyMoveHandler _moveHandler;
        private EnemyFsm _fsm;

        public override void MakeCollizion(int damage)
            => TakeDamage(damage);

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        public Vector2 GetPosition()
            => transform.position;

        [Inject]
        private void Construct(EnemyDamageHandler damageHandler,
            EnemyMoveHandler moveHandler,
            EnemyFsm fsm)
        {
            _damageHandler = damageHandler;
            _moveHandler = moveHandler;
            _fsm = fsm;
            _damageHandler.OnDeath += InvokeDeath;
            _moveHandler.OnTrigger += InvokeDeactivate;
        }

        private void InvokeDeath()
        {
            OnDeath?.Invoke(this);
        }

        private void InvokeDeactivate(GameObject gameObject)
        {
            if (gameObject.tag != "Border")
                return;
            OnDeactivate?.Invoke(this);
        }

        private void Initialize(Vector2 spawnPoint,
            Vector2 direction,
            Transform parent)
        {
            _moveHandler.Direction = direction;
            transform.position = spawnPoint;
            transform.parent = parent;
            _fsm.Enter<Initialize>();
        }

        private void OnDestroy()
        {
            _damageHandler.OnDeath -= InvokeDeath;
            _moveHandler.OnTrigger -= InvokeDeactivate;
        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, EnemyHandler>
        {
            protected Transform _buffer;

            [Inject]
            private void Construct(EnemyBuffer buffer)
            {
                _buffer = buffer.transform;
            }

            protected override void OnCreated(EnemyHandler item)
            {
                item.transform.SetParent(_buffer);
                base.OnCreated(item);
            }

            protected override void Reinitialize(Vector2 spawnPoint, Vector2 direction, EnemyHandler item)
            {
                base.Reinitialize(spawnPoint, direction, item);
                item.Initialize(spawnPoint, direction, _buffer);
            }
        }
    }
}