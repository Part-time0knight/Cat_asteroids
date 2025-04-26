using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.Handlers
{
    public class MoveHandler : IPauseble, IInitializable, IDisposable
    {
        protected Vector2 Velocity 
        {
            get => _body.linearVelocity;
            set => _body.linearVelocity = value;
        }

        protected readonly Rigidbody2D _body;
        protected readonly Settings _stats;
        protected readonly IPauseHandler _pause;

        protected readonly List<RaycastHit2D> _raycastsX;
        protected readonly List<RaycastHit2D> _raycastsY;

        protected ContactFilter2D _filter;
        protected float _collisionOffset;
        protected float _collisionDistance;
        protected float _distanceBetween;
        protected Vector2 _closestColliderPoint;

        protected Vector2 _pausedVelocity;
        protected bool _paused;

        public MoveHandler(Rigidbody2D body, Settings stats, IPauseHandler pause)
        {
            _body = body;
            _stats = stats;
            _pause = pause;

            _filter = new();
            _raycastsX = new();
            _raycastsY = new();

            _collisionOffset = 0.1f;
            _stats.CurrentSpeed = _stats.Speed;
            _stats.CurrentPosition = _body.position;
            _paused = false;
        }

        public void Initialize()
        {
            _pause.SubscribeElement(this);
        }

        public void Dispose()
        {
            _pause.UnsubscribeElement(this);
        }

        public virtual void Move(Vector2 speedMultiplier)
        { 
            Velocity = CollisionCheck(speedMultiplier) *
            _stats.CurrentSpeed * PauseSpeed();
            _stats.CurrentPosition = _body.position;
        }


        public void Stop()
            => _body.linearVelocity = Vector2.zero;

        public virtual void OnPause(bool active)
        {
            _paused = active;
            if (_paused )
                Velocity = Vector2.zero;
        }

        protected virtual float PauseSpeed()
            => _paused == false ? 1f : 0f;

        protected virtual Vector2 CollisionCheck(Vector2 speedMultiplier)
        {
            _raycastsX.Clear();
            _raycastsY.Clear();
            _body.Cast(new(speedMultiplier.x, 0f), _filter, _raycastsX, speedMultiplier.magnitude * Time.fixedDeltaTime + _collisionOffset);
            _body.Cast(new(0f, speedMultiplier.y), _filter, _raycastsY, speedMultiplier.magnitude * Time.fixedDeltaTime + _collisionOffset);
            foreach (var ray in _raycastsX)
            {
                _collisionDistance = ray.distance;
                _closestColliderPoint = _body.ClosestPoint(ray.point);
                _distanceBetween = Vector2.Distance(_closestColliderPoint, ray.point) - _collisionOffset;
                speedMultiplier = new(speedMultiplier.normalized.x * _distanceBetween, speedMultiplier.y);
            }
            foreach (var ray in _raycastsY)
            {
                _collisionDistance = ray.distance;
                _closestColliderPoint = _body.ClosestPoint(ray.point);
                _distanceBetween = Vector2.Distance(_closestColliderPoint, ray.point) - _collisionOffset;
                speedMultiplier = new(speedMultiplier.x, speedMultiplier.normalized.y * _distanceBetween);
            }
            return speedMultiplier;
        }

        public class Settings
        {
            [field: SerializeField] public float Speed { get; protected set; }
            public float CurrentSpeed { get; set; }

            public Vector2 CurrentPosition { get; set; }

            public Settings()
            { }

            public Settings(float speed, float currentSpeed, Vector2 currentPosition)
            {
                Speed = speed;
                CurrentSpeed = currentSpeed;
                CurrentPosition = currentPosition;
            }

            public Settings(Settings settings) : this(
                settings.Speed,
                settings.CurrentSpeed,
                settings.CurrentPosition)
            { }
        }
    }
}