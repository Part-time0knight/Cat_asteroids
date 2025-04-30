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
        }


        public void Stop()
            => _body.linearVelocity = Vector2.zero;


        public class Settings
        {
            [field: SerializeField] public float Speed { get; protected set; }

            

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