using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Enemy.Ice
{
    public class IceMoveHandler : EnemyMoveHandler, IFixedTickable
    {
        private readonly IceSettings _iceSettings;
        private Vector2 _initialVelocity = Vector2.zero;
        private Vector2 _currentVelocity = Vector2.zero;
        private float _speedMultiplier = 1f;

        public IceMoveHandler(Rigidbody2D body, IceSettings stats) : base(body, stats)
        {
            _iceSettings = stats;
        }

        public void FixedTick()
        {
            Vector2 position = _body.position;

            float desiredYDirection = -Mathf.Sign(position.y); 


            _currentVelocity.y = Mathf.Lerp(_currentVelocity.y,
                desiredYDirection,
                _iceSettings.CorrectionSpeed * Time.fixedDeltaTime);

            _currentVelocity.Normalize();

            _body.linearVelocity = _currentVelocity * _iceSettings.Speed * _speedMultiplier;
        }

        public override void Pause()
        {
            _speedMultiplier = 0;
            base.Pause();
        }

        public override void Stop()
        {
            _speedMultiplier = 0;
            base.Stop();
        }

        public override void Continue()
        {
            _speedMultiplier = 1f;
            base.Continue();
        }

        public override void Move(Vector2 speedMultiplier)
        {
            _speedMultiplier = 1f;
            base.Move(speedMultiplier);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void SetInitialVelocity(Vector2 velocity)
        {
            _initialVelocity = velocity;
            _currentVelocity = velocity;
            _body.linearVelocity = _initialVelocity;
        }

        [Serializable]
        public class IceSettings : EnemySettings 
        {
            [field: SerializeField] public float CorrectionSpeed { get; private set; }
        }
    }
}