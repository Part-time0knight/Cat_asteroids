using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers;
using UnityEngine;

namespace Game.Logic.Player.Fsm.States
{
    public abstract class Hitable : IState
    {
        protected readonly IGameStateMachine _stateMachine;
        protected readonly PlayerDamageHandler _damageHandler;
        protected readonly PlayerWeaponHandler _weaponHandler;
        protected readonly PlayerMoveHandler _moveHandler;

        public Hitable(IGameStateMachine stateMachine,
            PlayerDamageHandler damageHandler,
            PlayerMoveHandler moveHandler,
            PlayerWeaponHandler weaponHandler)
        {
            _stateMachine = stateMachine;
            _damageHandler = damageHandler;
            _weaponHandler = weaponHandler;
            _moveHandler = moveHandler;
        }

        public virtual void OnEnter()
        {
            _damageHandler.OnDeath += OnHit;
            _moveHandler.OnCollision += MakeDamage;
        }

        public virtual void OnExit()
        {
            _damageHandler.OnDeath -= OnHit;
            _moveHandler.OnCollision -= MakeDamage;
        }

        protected virtual void OnHit()
        {
            _stateMachine.Enter<Dead>();
        }

        protected virtual void MakeDamage(GameObject gameObject)
        {
            var unit = gameObject.GetComponent<UnitHandler>();
            if (unit != null)
                _weaponHandler.TickableDamage(unit);
        }
    }
}