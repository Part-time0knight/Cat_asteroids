using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Effects.Explosion;
using Game.Logic.Handlers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Dead : IState
    {
        private readonly Rigidbody2D _body;
        private readonly ExplosionSpawner _spawner;
        private readonly EnemyHandler _enemyHandler;
        private List<Collider2D> _colliders = new();

        public Dead(Rigidbody2D body,
            ExplosionSpawner spawner,
            EnemyHandler enemyHandler) 
        { 
            _body = body;
            _spawner = spawner;
            _enemyHandler = enemyHandler;
        }

        public void OnEnter()
        {
            _body.GetAttachedColliders(_colliders);
            _colliders.ForEach((collider) => collider.enabled = false);
            _spawner.Spawn(_body.position, _enemyHandler.Size);
            _enemyHandler.InvokeDeath();
        }

        public void OnExit()
        {
            _colliders.ForEach((collider) => collider.enabled = true);
        }
    }
}