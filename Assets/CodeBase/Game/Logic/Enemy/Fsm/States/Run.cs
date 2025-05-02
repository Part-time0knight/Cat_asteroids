using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Enemy.Asteroid;
using Game.Logic.Handlers;
using UnityEngine;

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
            _moveHandler.OnCollision += Hit;
            _damageHandler.OnDeath += OnDeath;
            _moveHandler.OnTrigger += OnDisable;
            _moveHandler.Move();
            _rotate.Play();
        }

        public void OnExit()
        {
            _moveHandler.OnCollision -= Hit;
            _moveHandler.OnTrigger -= OnDisable;
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