using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Handlers;
using UnityEngine;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Run : IState
    {
        protected readonly IGameStateMachine _stateMachine;

        protected readonly EnemyMoveHandler _moveHandler;
        protected readonly EnemyWeaponHandler _weapon;
        protected readonly EnemyDamageHandler _damageHandler;
        protected readonly EnemyFacade _enemyHandler;

        public Run(IGameStateMachine stateMachine,
            EnemyFacade enemyHandler,
            EnemyMoveHandler moveHandler,
            EnemyWeaponHandler weapon,
            EnemyDamageHandler damageHandler)
        {
            _stateMachine = stateMachine;
            _enemyHandler = enemyHandler;
            _moveHandler = moveHandler;
            _weapon = weapon;
            _damageHandler = damageHandler;
            
        }

        public virtual void OnEnter()
        {
            //Debug.Log("Enter in Run state " + GetType());
            _moveHandler.OnCollision += Hit;
            _damageHandler.OnDeath += InvokeDeath;
            _moveHandler.OnTrigger += InvokeTrigger;
            _enemyHandler.OnDamaged += InvokeDamaged;
            _enemyHandler.OnPause += InvokePause;
            _moveHandler.Move(_enemyHandler.Direction);
            InvokePause(_enemyHandler.Pause);
        }

        public virtual void OnExit()
        {
            _moveHandler.OnCollision -= Hit;
            _moveHandler.OnTrigger -= InvokeTrigger;
            _damageHandler.OnDeath -= InvokeDeath;
            _enemyHandler.OnDamaged -= InvokeDamaged;
            _enemyHandler.OnPause -= InvokePause;
            _moveHandler.Stop();
        }

        protected void Hit(GameObject gameObject)
        {
            var target = gameObject.GetComponent<UnitFacade>();
            if (target == null) return;
            _weapon.FrameDamage(target);
        }

        protected void InvokeDamaged(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        protected void InvokeDeath()
        {
            _stateMachine.Enter<Dead>();
        }

        protected void InvokeTrigger(GameObject gameObject)
        {
            if (gameObject.tag == "Border")
                _enemyHandler.InvokeDeactivate();
            if (gameObject.tag == "Projectile")
                Hit(gameObject);
        }

        protected void InvokePause(bool pause)
        {
            if (!pause) return;
            _stateMachine.Enter<Pause>();
        }
    }
}