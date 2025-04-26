using Game.Logic.Handlers;
using Game.Logic.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Enemy
{
    public class EnemyMoveHandler : MoveHandler
    {
        public Action<GameObject> InvokeCollision;

        private readonly PlayerMoveHandler.PlayerSettings _playerSettings;
        private readonly Animator _animator;
        private readonly List<RaycastHit2D> _raycasts;

        private Vector2 _playerDirection;
        private Vector3 _standartScale;

        public EnemyMoveHandler(Rigidbody2D body,
            EnemySettingsHandler stats,
            IPauseHandler pause,
            PlayerMoveHandler.PlayerSettings playerSettings,
            Animator animator)
            : base(body, stats.MoveSettings)
        {
            _playerSettings = playerSettings;
            _playerDirection = Vector2.zero;
            _animator = animator;
            _standartScale = new(
                _animator.transform.localScale.x,
                _animator.transform.localScale.y,
                _animator.transform.localScale.z);
            _raycasts = new();
        }

        public override void Move(Vector2 speedMultiplier)
        {
            base.Move(speedMultiplier);
            Vector3 scale = new(
                _standartScale.x * Mathf.Sign(speedMultiplier.x),
                _standartScale.y, _standartScale.z);
            _animator.transform.localScale = scale;
        }

        public void MoveToPlayer()
        {
            _playerDirection = (_playerSettings.CurrentPosition - _body.position).normalized;
            Move(_playerDirection);
        }

        /*protected override Vector2 CollisionCheck(Vector2 speedMultiplier)
        {
            _body.Cast(speedMultiplier, _filter, _raycasts, _stats.CurrentSpeed * Time.fixedDeltaTime + _collisionOffset);

            foreach (var hit in _raycasts)
                InvokeCollision?.Invoke(hit.transform.gameObject);
            return speedMultiplier;
        }*/

        [Serializable]
        public class EnemySettings : Settings
        {
            public EnemySettings(Settings settings) : base(settings)
            { }
        }
    }
}