using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using UnityEngine;

namespace Game.Logic.Player.Fsm.States
{
    public abstract class Hitable : IState
    {
        protected readonly IGameStateMachine _stateMachine;
        protected readonly PlayerFacade _playerFacade;
        protected readonly IHandlerGetter _handlerGetter;


        public Hitable(IGameStateMachine stateMachine,
            PlayerFacade playerFacade,
            IHandlerGetter handlerGetter)
        {
            _stateMachine = stateMachine;
            _handlerGetter = handlerGetter;
            _playerFacade = playerFacade;
        }

        public virtual void OnEnter()
        {
            _playerFacade.OnTakeDamage +=
                _handlerGetter.Get<IPlayerDamageHandler>().TakeDamage;

            _handlerGetter.Get<IInvincibilityHandler>().OnPowerChange += InvokePower;

            _handlerGetter.Get<IPlayerDamageHandler>().OnTakeDamage += InvokeHit;
            _handlerGetter.Get<IPlayerDamageHandler>().OnDeath += InvokeDead;
            _handlerGetter.Get<IPlayerMoveHandler>().OnCollision += MakeDamage;
            _handlerGetter.Get<IPlayerMoveHandler>().OnTrigger += MakeDamage;
        }

        public virtual void OnExit()
        {
            _playerFacade.OnTakeDamage -= _handlerGetter.Get<IPlayerDamageHandler>().TakeDamage;

            _handlerGetter.Get<IInvincibilityHandler>().OnPowerChange -= InvokePower;

            _handlerGetter.Get<IPlayerDamageHandler>().OnTakeDamage -= InvokeHit;
            _handlerGetter.Get<IPlayerDamageHandler>().OnDeath -= InvokeDead;
            _handlerGetter.Get<IPlayerMoveHandler>().OnCollision -= MakeDamage;
            _handlerGetter.Get<IPlayerMoveHandler>().OnTrigger -= MakeDamage;
        }

        protected virtual void InvokeDead()
        {
            _stateMachine.Enter<Dead>();
        }

        protected virtual void InvokeHit(int damage)
        {
            _handlerGetter.Get<IInvincibilityHandler>().Start();
        }

        protected virtual void MakeDamage(GameObject gameObject)
        {
            var unit = gameObject.GetComponent<UnitFacade>();
            if (unit != null)
                _handlerGetter.Get<IWeaponHandler>().FrameDamage(unit);
        }

        protected virtual void InvokePower(bool power)
        {
            Debug.Log("Power: " +  power);
            _handlerGetter.Get<IPlayerDamageHandler>().Power = power;
        }
    }
}