using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Animation;
using UnityEngine;

namespace Game.Logic.Player.Fsm.States
{
    public abstract class Hitable : IState
    {
        protected readonly IGameStateMachine _stateMachine;
        protected readonly PlayerDamageHandler _damageHandler;
        protected readonly PlayerWeaponHandler _weaponHandler;
        protected readonly PlayerTakeDamage _takeDamageAnimation;
        protected readonly PlayerHandler _playerHandler;
        protected readonly PlayerInvincibilityHandler _playerInvincibility;
        protected readonly IHandlerGetter _handlerGetter;


        public Hitable(IGameStateMachine stateMachine,
            PlayerDamageHandler damageHandler,
            PlayerWeaponHandler weaponHandler,
            PlayerTakeDamage playerTakeDamage,
            PlayerHandler playerHandler,
            PlayerInvincibilityHandler playerInvincibility,
            IHandlerGetter handlerGetter)
        {
            _stateMachine = stateMachine;
            _damageHandler = damageHandler;
            _weaponHandler = weaponHandler;
            _handlerGetter = handlerGetter;
            _takeDamageAnimation = playerTakeDamage;
            _playerHandler = playerHandler;
            _playerInvincibility = playerInvincibility;
        }

        public virtual void OnEnter()
        {
            _playerHandler.OnTakeDamage += _damageHandler.TakeDamage;

            _damageHandler.OnTakeDamage += InvokeHit;
            _damageHandler.OnDeath += InvokeDead;
            _handlerGetter.Get<IPlayerMoveHandler>().OnCollision += MakeDamage;
        }

        public virtual void OnExit()
        {

            _playerHandler.OnTakeDamage -= _damageHandler.TakeDamage;

            _damageHandler.OnTakeDamage -= InvokeHit;
            _damageHandler.OnDeath -= InvokeDead;
            _handlerGetter.Get<IPlayerMoveHandler>().OnCollision -= MakeDamage;
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