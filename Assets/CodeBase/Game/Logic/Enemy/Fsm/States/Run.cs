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
        private readonly EnemyHandler _enemyHandler;

        public Run(IGameStateMachine stateMachine,
            EnemyHandler enemyHandler,
            EnemyMoveHandler moveHandler,
            EnemyWeaponHandler weapon,
            EnemyDamageHandler damageHandler,
            AsteroidRotate rotate)
        {
            _stateMachine = stateMachine;
            _enemyHandler = enemyHandler;
            _moveHandler = moveHandler;
            _weapon = weapon;
            _damageHandler = damageHandler;
            _rotate = rotate;
        }

        public void OnEnter()
        {
            _moveHandler.OnCollision += Hit;
            _damageHandler.OnDeath += InvokeDeath;
            _moveHandler.OnTrigger += InvokeDisable;
            _enemyHandler.OnDamaged += InvokeDamaged;
            _moveHandler.Move(_enemyHandler.Direction);
            _rotate.Play();
        }

        public void OnExit()
        {
            _moveHandler.OnCollision -= Hit;
            _moveHandler.OnTrigger -= InvokeDisable;
            _damageHandler.OnDeath -= InvokeDeath;
            _enemyHandler.OnDamaged -= InvokeDamaged;
            _moveHandler.Stop();
            _rotate.Stop();
        }

        private void Hit(GameObject gameObject)
        {
            var target = gameObject.GetComponent<UnitHandler>();
            if (target == null) return;
            _weapon.TickableDamage(target);
        }

        private void InvokeDamaged(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        private void InvokeDeath()
        {
            _stateMachine.Enter<Dead>();
        }

        private void InvokeDisable(GameObject gameObject)
        {
            if (gameObject.tag != "Border")
                return;
            _enemyHandler.InvokeDeactivate();
        }
    }
}