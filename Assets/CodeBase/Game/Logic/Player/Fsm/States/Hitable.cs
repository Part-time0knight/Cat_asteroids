using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers;
using Game.Logic.Player.Animation;
using UnityEngine;

namespace Game.Logic.Player.Fsm.States
{
    public abstract class Hitable : IState
    {
        protected readonly IGameStateMachine _stateMachine;
        protected readonly PlayerDamageHandler _damageHandler;
        protected readonly PlayerWeaponHandler _weaponHandler;
        protected readonly PlayerMoveHandler _moveHandler;
        protected readonly PlayerTakeDamage _takeDamageAnimation;

        public Hitable(IGameStateMachine stateMachine,
            PlayerDamageHandler damageHandler,
            PlayerMoveHandler moveHandler,
            PlayerWeaponHandler weaponHandler,
            PlayerTakeDamage playerTakeDamage)
        {
            _stateMachine = stateMachine;
            _damageHandler = damageHandler;
            _weaponHandler = weaponHandler;
            _moveHandler = moveHandler;
            _takeDamageAnimation = playerTakeDamage;
        }

        public virtual void OnEnter()
        {
            _damageHandler.OnDeath += InvokeDead;
            _damageHandler.OnTakeDamage += InvokeHit;
            _moveHandler.OnCollision += MakeDamage;
        }

        public virtual void OnExit()
        {
            _damageHandler.OnTakeDamage -= InvokeHit;
            _damageHandler.OnDeath -= InvokeDead;
            _moveHandler.OnCollision -= MakeDamage;
        }

        protected virtual void InvokeDead()
        {
            _stateMachine.Enter<Dead>();
        }

        protected virtual void InvokeHit(int damage)
        {
            _takeDamageAnimation.Play();
        }

        protected virtual void MakeDamage(GameObject gameObject)
        {
            var unit = gameObject.GetComponent<UnitHandler>();
            if (unit != null)
                _weaponHandler.TickableDamage(unit);
        }
    }
}