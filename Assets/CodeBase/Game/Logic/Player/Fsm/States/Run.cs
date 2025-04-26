using Core.Infrastructure.GameFsm;
using Game.Logic.StaticData;
using UnityEngine;


namespace Game.Logic.Player.Fsm.States
{
    public class Run : Hitable
    {
        private readonly PlayerShootHandler _playerShoot;
        private readonly PlayerInput _playerInput;
        private readonly PlayerMoveHandler _playerMove;

        public Run(IGameStateMachine stateMachine,
            PlayerInput playerInput,
            PlayerMoveHandler playerMove,
            PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerShootHandler playerShoot) : base(stateMachine, damageSettings)
        {
            _playerInput = playerInput;
            _playerMove = playerMove;
            _playerShoot = playerShoot;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _playerInput.InvokeMoveButtonsUp += OnMoveEnd;
            _playerInput.InvokeMove += Move;

            _playerShoot.StartAutomatic();
        }

        private void Move(Vector2 direction)
        {
            _playerMove.Move(direction);
        }

        private void OnMoveEnd()
        {
            _playerMove.Stop();
            _stateMachine.Enter<Idle>();

        }

        protected override void OnHit()
        {
            base.OnHit();
            _playerMove.Stop();
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerInput.InvokeMoveButtonsUp -= OnMoveEnd;
            _playerInput.InvokeMove -= Move;
            _playerShoot.StopAutomatic();
        }


    }
}