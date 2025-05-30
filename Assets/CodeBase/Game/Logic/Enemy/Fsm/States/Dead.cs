using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Effects.Explosion;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Dead : IState
    {
        private readonly IGameStateMachine _fsm;
        private readonly Rigidbody2D _body;
        private readonly ExplosionSpawner _spawner;
        private readonly EnemyFacade _enemyHandler;
        private List<Collider2D> _colliders = new();

        public Dead(IGameStateMachine fsm,
            Rigidbody2D body,
            ExplosionSpawner spawner,
            EnemyFacade enemyHandler) 
        {
            _fsm = fsm;
            _body = body;
            _spawner = spawner;
            _enemyHandler = enemyHandler;
        }

        public void OnEnter()
        {
            _spawner.Spawn(_body.position, _enemyHandler.Size);
            _enemyHandler.CallDeathInvoke();
            _fsm.Enter<Disable>();
        }

        public void OnExit()
        {
            _colliders.ForEach((collider) => collider.enabled = true);
        }
    }
}