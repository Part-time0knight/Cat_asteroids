using Game.Logic.Effects.Particles;
using Game.Logic.Misc;
using System;
using UnityEngine;

namespace Game.Logic.Player.Handlers
{
    public class RamMoveHandler : PlayerBaseMoveHandler, IDisposable
    {
        private readonly Timer _ramTimer = new();
        private readonly float _angularDamping;
        private readonly float _mass;

        public RamMoveHandler(Rigidbody2D body,
            PlayerHasteEffect hasteEffect,
            PlayerSettings stats,
            IPlayerPositionWriter dataWriter) : base(body,
                hasteEffect,
                stats,
                dataWriter)
        {
            _angularDamping = _body.angularDamping;
            _mass = _body.mass;
        }

        public void Ram(float speed, float duration, float mass)
        {
            _ramTimer.Initialize(duration, InvokeEndRam).Play();
            _body.mass = mass;
            _body.angularDamping = float.MaxValue;
            _needMaxSpeed = false;
            Move(_body.transform.TransformDirection(Vector2.up) * speed);
        }

        public override void Pause()
        {
            _ramTimer.Pause();
            base.Pause();
        }

        public override void Continue()
        {
            _ramTimer.Play();
            base.Continue();
        }

        public override void Move()
        {
            if (_ramTimer.Active) return;
            base.Move();
        }

        public override void ReverseMove()
        {
            if (_ramTimer.Active) return;
            base.ReverseMove();
        }

        public override void Rotate(float horizontal)
        {
            if (_ramTimer.Active) return;
            base.Rotate(horizontal);
        }

        private void InvokeEndRam()
        {
            _body.mass = _mass;
            _body.angularDamping = _angularDamping;
            _needMaxSpeed = true; ;
        }

        public void Dispose()
        {
            InvokeEndRam();
        }
    }
}