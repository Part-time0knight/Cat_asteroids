using Game.Logic.Handlers;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerInput : ITickable, IFixedTickable, IPauseble, IInitializable, IDisposable
    {
        public event Action InvokeMoveButtonsDown;

        public event Action InvokeMoveButtonsUp;

        public event Action<float> InvokeMoveHorizontal;

        public event Action<float> InvokeMoveVertical;

        public event Action<Vector2> InvokeMove;

        //public event Action InvokeAttackButton;
        public event Action InvokeSpellButton;

        private bool _isHorizontal;
        private bool _isVertical;

        private bool _pause;
        private bool _buttonUp = false;

        private IPauseHandler _pauseHandler;

        public float Horizontal => OnMoveHorizontal();
        public float Vertical => OnMoveVertical();

        public bool IsMoveButtonPress
        {
            get => Input.GetButton("Vertical")
                || Input.GetButton("Horizontal");
        }

        public PlayerInput(IPauseHandler pauseHandler)
        {
            _pauseHandler = pauseHandler;
        }

        public void Initialize()
        {
            _pauseHandler.SubscribeElement(this);
        }


        public void Dispose()
        {
            _pauseHandler.UnsubscribeElement(this);
        }

        public Vector2 MousePosition()
            => Camera.main.ScreenToWorldPoint(
                new(Input.mousePosition.x,
                    Input.mousePosition.y,
                    Camera.main.nearClipPlane));

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

        private bool OnAttackButton()
            => Input.GetButton("Fire1");

        private bool OnSpellButton()
            => Input.GetButton("Spell");

        public void Tick()
        {
            if (_pause)
            {
                if (OnMoveButtonUp())
                    _buttonUp = true;
                return;
            }
            if (OnMoveButtonDown())
                InvokeMoveButtonsDown?.Invoke();
            if (OnMoveButtonUp())
                InvokeMoveButtonsUp?.Invoke();

            //if (OnAttackButton())
                //InvokeAttackButton?.Invoke();
            if (OnSpellButton())
                InvokeSpellButton?.Invoke();
        }

        public void FixedTick()
        {
            if (_pause)
                return;

            _isHorizontal = OnMoveHorizontal() != 0;
            _isVertical = OnMoveVertical() != 0;
            if (_isHorizontal)
                InvokeMoveHorizontal?.
                    Invoke(OnMoveHorizontal());
            if (_isVertical)
                InvokeMoveVertical?.
                    Invoke(OnMoveVertical());

            InvokeMove?.Invoke(new(OnMoveHorizontal(), OnMoveVertical()));
        }

        public void OnPause(bool active)
        {
            _pause = active;
            if (!active && _buttonUp)
            {
                if (!OnMoveButtonDown())
                    InvokeMoveButtonsUp?.Invoke();
                _buttonUp = false;
            }
        }
    }
}