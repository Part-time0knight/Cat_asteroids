using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers;
using Game.Logic.Misc;
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
        protected readonly PlayerHandler _playerHandler;
        protected readonly PlayerInvincibilityHandler _playerInvincibility;


        public Hitable(IGameStateMachine stateMachine,
            PlayerDamageHandler damageHandler,
            PlayerMoveHandler moveHandler,
            PlayerWeaponHandler weaponHandler,
            PlayerTakeDamage playerTakeDamage,
            PlayerHandler playerHandler,
            PlayerInvincibilityHandler playerInvincibility)
        {
            _stateMachine = stateMachine;
            _damageHandler = damageHandler;
            _weaponHandler = weaponHandler;
            _moveHandler = moveHandler;
            _takeDamageAnimation = playerTakeDamage;
            _playerHandler = playerHandler;
            _playerInvincibility = playerInvincibility;
        }

        public virtual void OnEnter()
        {
            _playerHandler.OnTakeDamage += _damageHandler.TakeDamage;

            _damageHandler.OnTakeDamage += InvokeHit;
            _damageHandler.OnDeath += InvokeDead;
            _moveHandler.OnCollision += MakeDamage;
        }

        public virtual void OnExit()
        {

            _playerHandler.OnTakeDamage -= _damageHandler.TakeDamage;

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
            _playerInvincibility.Start();
        }

        protected virtual void MakeDamage(GameObject gameObject)
        {
            var unit = gameObject.GetComponent<UnitHandler>();
            if (unit != null)
                _weaponHandler.TickableDamage(unit);
        }
    }
}