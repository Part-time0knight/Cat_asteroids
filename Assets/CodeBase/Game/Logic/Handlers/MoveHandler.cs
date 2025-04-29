using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.Handlers
{
    public class MoveHandler
    {
        protected Vector2 Velocity 
        {
            get => _body.linearVelocity;
            set => _body.linearVelocity = value;
        }

        protected readonly Rigidbody2D _body;
        protected readonly Settings _stats;


        public MoveHandler(Rigidbody2D body, Settings stats)
        {
            _body = body;
            _stats = stats;
        }

        public virtual void Move(Vector2 speedMultiplier)
        {
            _body.AddForce(speedMultiplier * _stats.Speed, ForceMode2D.Impulse);
            if (_body.linearVelocity.magnitude > _stats.MaxSpeed)
                _body.linearVelocity = _body.linearVelocity.normalized * _stats.MaxSpeed;
        }


        public void Stop()
            => _body.linearVelocity = Vector2.zero;

        /*protected virtual Vector2 CollisionCheck(Vector2 speedMultiplier)
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
        }*/

        public class Settings
        {
            [field: SerializeField] public float Speed { get; protected set; }

            [field: SerializeField] public float MaxSpeed { get; protected set; }

            public Settings()
            { }

            public Settings(float speed)
            {
                Speed = speed;
            }

            public Settings(Settings settings) : this(
                settings.Speed)
            { }
        }
    }
}