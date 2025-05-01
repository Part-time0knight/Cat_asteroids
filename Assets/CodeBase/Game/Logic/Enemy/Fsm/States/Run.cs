using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Enemy.Asteroid;
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

        private readonly EnemyMoveHandler _moveHandler;
        private readonly EnemyWeaponHandler _weapon;
        private readonly EnemyDamageHandler _damageHandler;
        private readonly AsteroidRotate _rotate;

        public Run(IGameStateMachine stateMachine,
            EnemyMoveHandler moveHandler,
            EnemyWeaponHandler weapon,
            EnemyDamageHandler damageHandler,
            AsteroidRotate rotate)
        {
            _stateMachine = stateMachine;

            _moveHandler = moveHandler;
            _weapon = weapon;
            _damageHandler = damageHandler;
            _rotate = rotate;
        }

        public void OnEnter()
        {
            _moveHandler.InvokeCollision += Hit;
            _damageHandler.OnDeath += OnDeath;
            _moveHandler.InvokeTrigger += OnDisable;
            _moveHandler.Move();
            _rotate.Play();
        }

        public void OnExit()
        {
            _moveHandler.InvokeCollision -= Hit;
            _moveHandler.InvokeTrigger -= OnDisable;
            _damageHandler.OnDeath -= OnDeath;
            _moveHandler.Stop();
            _rotate.Stop();
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