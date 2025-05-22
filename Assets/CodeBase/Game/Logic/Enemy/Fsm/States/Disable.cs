using Core.Infrastructure.GameFsm.States;
using Game.Logic.Effects.Explosion;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Disable : IState
    {
        private readonly Rigidbody2D _body;
        private readonly EnemyHandler _enemyHandler;
        private List<Collider2D> _colliders = new();

        public Disable(Rigidbody2D body,
            EnemyHandler enemyHandler)
        {
            _body = body;
            _enemyHandler = enemyHandler;
        }

        public void OnEnter()
        {
            //Debug.Log("Enter in Disable state " + GetType());
            _body.GetAttachedColliders(_colliders);
            _colliders.ForEach((collider) => collider.enabled = false);
        }

        public void OnExit()
        {
            _colliders.ForEach((collider) => collider.enabled = true);
        }
    }
}