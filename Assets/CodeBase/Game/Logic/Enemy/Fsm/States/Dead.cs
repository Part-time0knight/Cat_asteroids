using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Effects.Explosion;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Dead : IState
    {
        private readonly Rigidbody2D _body;
        private readonly ExplosionSpawner _spawner;
        private List<Collider2D> _colliders = new();

        public Dead(Rigidbody2D body, ExplosionSpawner spawner) 
        { 
            _body = body;
            _spawner = spawner;
        }


        public void OnEnter()
        {
            _body.GetAttachedColliders(_colliders);
            _colliders.ForEach((collider) => collider.enabled = false);
            _spawner.Spawn(_body.position);
        }

        public void OnExit()
        {
            _colliders.ForEach((collider) => collider.enabled = true);
        }
    }
}