using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Infrastructure;
using Game.Infrastructure.States;
using Game.Logic.Effects.Explosion;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Player.Fsm.States
{
    public class Dead : IState
    {
        private readonly ExplosionSpawner _explosionSpawner;
        private readonly Rigidbody2D _body;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly IGameStateMachine _gameFsm;

        private List<Collider2D> _colliders = new();

        public Dead(ExplosionSpawner explosionSpawner,
            Rigidbody2D body,
            SpriteRenderer spriteRenderer,
            GameFsm gameFsm)
        {
            _explosionSpawner = explosionSpawner;
            _body = body;
            _spriteRenderer = spriteRenderer;
            _gameFsm = gameFsm;
        }

        public void OnEnter()
        {
            _body.GetAttachedColliders(_colliders);
            _colliders.ForEach((collider) => collider.enabled = false);
            _explosionSpawner.Spawn(_body.position);
            _spriteRenderer.enabled = false;
            _gameFsm.Enter<Defeat>();
        }

        public void OnExit()
        {
            _colliders.ForEach((collider) => collider.enabled = true);
            _spriteRenderer.enabled = true;
        }
    }
}
