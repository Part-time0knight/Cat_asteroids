using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player.Handlers
{
    public class PlayerInputHandler : ITickable, IFixedTickable, IInputHandler
    {
        public event Action InvokeMoveButtonsDown;

        public event Action InvokeMoveButtonsUp;

        public event Action<float> InvokeMoveHorizontal;

        public event Action<float> InvokeMoveVertical;

        private bool _isHorizontal;
        private bool _isVertical;

        public bool IsMoveButtonPress
        {
            get => Input.GetButton("Vertical")
                || Input.GetButton("Horizontal");
        }

        public PlayerInputHandler()
        {
        }

        private bool OnMoveButtonDown()
            => Input.GetButtonDown("Vertical")
                || Input.GetButtonDown("Horizontal");

        private bool OnMoveButtonUp()
            => (Input.GetButtonUp("Vertical")
                || Input.GetButtonUp("Horizontal"))
                && !IsMoveButtonPress;

        private float OnMoveVertical()
            => Input.GetAxis("Vertical");

        private float OnMoveHorizontal()
            => Input.GetAxis("Horizontal");

        public void Tick()
        {
            if (OnMoveButtonDown())
                InvokeMoveButtonsDown?.Invoke();
            if (OnMoveButtonUp())
                InvokeMoveButtonsUp?.Invoke();
        }

        public void FixedTick()
        {
            _isHorizontal = OnMoveHorizontal() != 0;
            _isVertical = OnMoveVertical() != 0;
            if (_isHorizontal)
                InvokeMoveHorizontal?.
                    Invoke(OnMoveHorizontal());
            if (_isVertical)
                InvokeMoveVertical?.
                    Invoke(OnMoveVertical());

        }
    }
}