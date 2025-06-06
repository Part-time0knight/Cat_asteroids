using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Infrastructure;
using Game.Infrastructure.States.Gameplay;
using Game.Logic.Effects.Explosion;
using Game.Logic.Handlers.Strategy;
using Game.Logic.Player.Handlers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Player.Fsm.States
{
    public class Dead : IState
    {
        private readonly ExplosionSpawner _explosionSpawner;
        private readonly PlayerFacade _playerHandler;
        private readonly Rigidbody2D _body;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly IGameStateMachine _gameFsm;
        private readonly IHandlerGetter _handlerGetter;

        private List<Collider2D> _colliders = new();

        public Dead(ExplosionSpawner explosionSpawner,
            PlayerFacade playerHandler,
            Rigidbody2D body,
            SpriteRenderer spriteRenderer,
            GameFsm gameFsm,
            IHandlerGetter handlerGetter)
        {
            _explosionSpawner = explosionSpawner;
            _playerHandler = playerHandler;
            _body = body;
            _spriteRenderer = spriteRenderer;
            _handlerGetter = handlerGetter;
            _gameFsm = gameFsm;
        }

        public void OnEnter()
        {
            _body.GetAttachedColliders(_colliders);
            _body.linearVelocity = Vector2.zero;
            _body.angularVelocity = 0;
            _colliders.ForEach((collider) => collider.enabled = false);
            _explosionSpawner.Spawn(_body.position, _playerHandler.Size);
            _spriteRenderer.enabled = false;
            _handlerGetter.Get<IInvincibilityHandler>().Stop();
            _gameFsm.Enter<Defeat>();
        }

        public void OnExit()
        {
            _colliders.ForEach((collider) => collider.enabled = true);
            _spriteRenderer.enabled = true;
        }
    }
}
