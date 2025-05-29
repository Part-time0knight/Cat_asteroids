using Game.Logic.Enemy.Fsm;
using Game.Logic.Enemy.Fsm.States;
using Game.Logic.Handlers;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy
{
    public abstract class EnemyHandler : UnitFacade
    {
        public event Action<bool> OnPause;
        public event Action<int> OnDamaged;

        public event Action<EnemyHandler> OnDeath;
        public event Action<EnemyHandler> OnDeactivate;

        protected EnemyFsm _fsm;
        private bool _pause = false;

        public override bool Pause
        { 
            get => _pause;
            set 
            { 
                _pause = value;
                OnPause?.Invoke(value);
            }
        }

        public virtual Vector2 Direction { get; private set; }

        public virtual void Clear()
        {

        }

        public override void MakeCollision(int damage)
            => TakeDamage(damage);

        public void TakeDamage(int damage)
        {
            OnDamaged?.Invoke(damage);
        }

        public Vector2 GetPosition()
            => transform.position;

        public void CallDeathInvoke()
        {
            OnDeath?.Invoke(this);
        }

        public void InvokeDeactivate()
        {
            OnDeactivate?.Invoke(this);
        }

        public void Kill()
        {
            _fsm.Enter<Dead>();
            Clear();
        }

        [Inject]
        protected virtual void Construct(EnemyFsm fsm)
        {
            _fsm = fsm;
        }

        protected virtual void Initialize(Vector2 spawnPoint,
            Vector2 direction)
        {
            Direction = direction;
            transform.position = spawnPoint;
            _fsm.Enter<Initialize>();
        }

        protected virtual void Deactivate()
        {
            _fsm.Enter<Disable>();
        }

        public class Pool : MonoMemoryPool<Vector2, Vector2, EnemyHandler>
        {
            protected override void OnDespawned(EnemyHandler item)
            {
                base.OnDespawned(item);
                item.Deactivate();
            }

            protected override void Reinitialize(Vector2 spawnPoint,
                Vector2 direction,
                EnemyHandler item)
            {
                base.Reinitialize(spawnPoint, direction, item);
                item.Initialize(spawnPoint, direction);
            }
        }
    }
}