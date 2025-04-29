using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Handlers;
using Game.Logic.StaticData;
using Game.Presentation.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Run : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowFsm _windowFsm;

        private readonly EnemyMoveHandler _moveHandler;
        private readonly EnemyWeaponHandler _weapon;
        private readonly EnemyDamageHandler _damageHandler;

        public Run(IGameStateMachine stateMachine,
            IWindowFsm windowFsm,
            EnemyMoveHandler moveHandler,
            EnemyWeaponHandler weapon,
            EnemyDamageHandler damageHandler)
        {
            _stateMachine = stateMachine;
            _windowFsm = windowFsm;

            _moveHandler = moveHandler;
            _weapon = weapon;
            _damageHandler = damageHandler;
        }

        public void OnEnter()
        {
            _moveHandler.InvokeCollision += Hit;
            _damageHandler.OnDeath += OnDeath;
            _moveHandler.InvokeTrigger += OnDisable;
            _moveHandler.Move();
        }

        public void OnExit()
        {
            _moveHandler.InvokeCollision -= Hit;
            _moveHandler.InvokeTrigger -= OnDisable;
            _damageHandler.OnDeath -= OnDeath;
            _moveHandler.Stop();
        }

        private void Hit(GameObject gameObject)
        {
            var target = gameObject.GetComponent<UnitHandler>();
            if (target == null) return;
            _weapon.TickableDamage(target);
        }

        private void OnDeath()
        {
            _stateMachine.Enter<Dead>();
        }

        private void OnDisable(GameObject gameObject)
        {
            if (gameObject.tag != "Border")
                return;

        }
    }
}