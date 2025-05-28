using Game.Logic.Effects.Particles;
using Game.Logic.Handlers;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player.Handlers
{
    public class PlayerBaseMoveHandler : MoveHandler, IFixedTickable, IPlayerMoveHandler
    {
        private readonly PlayerSettings _playerSettings;
        private readonly IPlayerPositionWriter _playerDataWriter;
        private readonly PlayerHasteEffect _hasteEffect;

        public PlayerBaseMoveHandler(Rigidbody2D body,
            PlayerHasteEffect hasteEffect,
            PlayerSettings stats,
            IPlayerPositionWriter dataWriter) : base(body, stats)
        {
            _playerSettings = stats;
            _playerDataWriter = dataWriter;
            _hasteEffect = hasteEffect;
        }

        public void Move()
        {
            Move(_body.transform.up * Time.fixedDeltaTime);
            _hasteEffect.InvokeHaste(_body.linearVelocity.magnitude);
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
            _playerDataWriter.MakeMove = true;
        }

        public void Rotate(float horizontal)
        {
            _body.angularVelocity = 0;
            _body.MoveRotation(_body.rotation + horizontal * _playerSettings.RotateSpeed * Time.fixedDeltaTime * -1f);
            _playerDataWriter.MakeMove = true;
        }

        public void FixedTick()
        {
            _playerDataWriter.Position = _body.transform.position;
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