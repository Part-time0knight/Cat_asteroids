using Game.Logic.Handlers;
using Game.Logic.Misc;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerMoveHandler : MoveHandler
    {

        private readonly PlayerSettings _playerSettings;
        private readonly IPlayerDataWriter _playerDataWriter;
        private readonly Timer _timer;

        public PlayerMoveHandler(Rigidbody2D body,
            PlayerSettings stats, IPlayerDataWriter dataWriter) : base(body, stats)
        {
            _playerSettings = stats;
            _playerDataWriter = dataWriter;
            _timer = new();
        }

        public void Move()
        {
            base.Move(_body.transform.up * Time.fixedDeltaTime);
        }

        public void ReverseMove()
        {
            base.Move(_body.transform.up * -1f * _playerSettings.ReverseSpeedMultiplier * Time.fixedDeltaTime);
        }

        public override void Move(Vector2 speedMultiplier)
        {
            base.Move(speedMultiplier);
            if (Mathf.Abs(_body.linearVelocity.magnitude) > _playerSettings.MaxSpeed) 
                _body.linearVelocity = _body.linearVelocity.normalized * _playerSettings.MaxSpeed;
            _playerDataWriter.Position = _body.transform.position;
        }

        public void Rotate(float horizontal)
        {
            _body.angularDamping = _playerSettings.RotateDampingOnPress;
            _body.MoveRotation(_body.rotation + horizontal * _playerSettings.RotateSpeed * Time.fixedDeltaTime * -1f);
            
        }

        [Serializable]
        public class PlayerSettings : Settings
        {
            [field: SerializeField, Range(0f, 1f)] 
            public float ReverseSpeedMultiplier { get; private set; }

            [field: SerializeField] public float MaxSpeed { get; protected set; }

            [field: SerializeField] public float RotateSpeed { get; private set; }

            [field: SerializeField] public float RotateDampingOnPress { get; protected set; }
            [field: SerializeField] public float RotateDampingOnFree { get; protected set; }
        }
    }
}