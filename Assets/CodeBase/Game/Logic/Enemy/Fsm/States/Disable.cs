using Core.Infrastructure.GameFsm.States;
using Game.Logic.Effects.Explosion;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Disable : IState
    {
        private readonly Rigidbody2D _body;
        private List<Collider2D> _colliders = new();

        public Disable(Rigidbody2D body,
            EnemyFacade enemyHandler)
        {
            _body = body;
        }

        public void OnEnter()
        {
            _body.simulated = false;
            _body.GetAttachedColliders(_colliders);
            _colliders.ForEach((collider) => collider.enabled = false);
        }

        public void OnExit()
        {
            _colliders.ForEach((collider) => collider.enabled = true);
            _body.simulated = true;
        }
    }
}