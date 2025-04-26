using Game.Logic.Handlers;
using System;
using UnityEngine;

namespace Game.Logic.Player
{
    public class PlayerMoveHandler : MoveHandler
    {

        private PlayerSettings _playerSettings;


        public PlayerMoveHandler(Rigidbody2D body,
            PlayerSettings stats) : base(body, stats)
        {
            _playerSettings = stats;
        }

        public void Move()
        {
            base.Move(_body.transform.up * Time.fixedDeltaTime);
        }

        public void ReverseMove()
        {
            base.Move(_body.transform.up * -1f * _playerSettings.ReverseSpeedMultiplier * Time.fixedDeltaTime);
        }

        public void Rotate(float horizontal)
        {
            _body.AddTorque(horizontal * _playerSettings.RotateSpeed * Time.fixedDeltaTime * -1f);
        }

        [Serializable]
        public class PlayerSettings : Settings
        {
            [field: SerializeField, Range(0f, 1f)] 
            public float ReverseSpeedMultiplier { get; private set; }

            [field: SerializeField] public float RotateSpeed { get; private set; }
        
        }
    }
}