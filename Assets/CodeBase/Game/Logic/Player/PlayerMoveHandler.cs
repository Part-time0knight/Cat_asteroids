using Game.Logic.Handlers;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerMoveHandler : MoveHandler
    {
        public event Action<float> OnHaste;

        private readonly PlayerSettings _playerSettings;
        private readonly IPlayerPositionWriter _playerDataWriter;

        public PlayerMoveHandler(Rigidbody2D body,
            PlayerSettings stats, IPlayerPositionWriter dataWriter) : base(body, stats)
        {
            _playerSettings = stats;
            _playerDataWriter = dataWriter;
        }

        public void Move()
        {
            Move(_body.transform.up * Time.fixedDeltaTime);
            OnHaste?.Invoke(_body.linearVelocity.magnitude);
        }

        public void ReverseMove()
        {
            Move(_body.transform.up
                * -1f 
                * _playerSettings.ReverseSpeedMultiplier
                * Time.fixedDeltaTime);
        }

        public override void Move(Vector2 speedMultiplier)
        {
            base.Move(speedMultiplier);
            if (Mathf.Abs(_body.linearVelocity.magnitude) > _playerSettings.MaxSpeed) 
                _body.linearVelocity = _body.linearVelocity.normalized * _playerSettings.MaxSpeed;
            _playerDataWriter.Position = _body.transform.position;
            _playerDataWriter.MakeMove = true;
        }

        public void Rotate(float horizontal)
        {
            _body.angularVelocity = 0;
            _body.MoveRotation(_body.rotation + horizontal * _playerSettings.RotateSpeed * Time.fixedDeltaTime * -1f);
            _playerDataWriter.MakeMove = true;
        }



        [Serializable]
        public class PlayerSettings : Settings
        {
            [field: SerializeField, Range(0f, 1f)] 
            public float ReverseSpeedMultiplier { get; private set; }

            [field: SerializeField] public float MaxSpeed { get; protected set; }

            [field: SerializeField] public float RotateSpeed { get; private set; }

        }
    }
}