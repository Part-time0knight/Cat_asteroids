using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Player;
using Game.Logic.StaticData;
using Game.Presentation.View;
using UnityEngine;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Run : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowFsm _windowFsm;

        private readonly EnemyMoveHandler _moveHandler;
        private readonly EnemyTickHandler _tickHandler;
        private readonly EnemyWeaponHandler _weapon;
        private readonly EnemyDamageHandler.EnemySettings _damageSettings;

        public Run(IGameStateMachine stateMachine,
            IWindowFsm windowFsm,
            EnemyMoveHandler moveHandler,
            EnemyTickHandler tickHandler,
            EnemyWeaponHandler weapon,
            EnemySettingsHandler settings)
        {
            _stateMachine = stateMachine;
            _windowFsm = windowFsm;

            _moveHandler = moveHandler;
            _tickHandler = tickHandler;
            _weapon = weapon;
            _damageSettings = settings.DamageSettings;
        }

        public void OnEnter()
        {
            _windowFsm.OpenWindow(typeof(EnemyView), inHistory: false);
            _tickHandler.OnFixedTick += UpdateMove;
            _damageSettings.InvokeHitPointsChange += OnHit;
            _moveHandler.InvokeCollision += HitPlayer;
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow(typeof(EnemyView));
            _tickHandler.OnFixedTick -= UpdateMove;
            _damageSettings.InvokeHitPointsChange -= OnHit;
            _moveHandler.InvokeCollision -= HitPlayer;
            _moveHandler.Stop();
        }

        private void HitPlayer(GameObject gameObject)
        {
            if (gameObject.tag != Tags.Player)
                return;
            _weapon.TickableDamage();
        }

        private void UpdateMove()
        {
            _moveHandler.MoveToPlayer();
        }

        private void OnHit()
        {
            if (_damageSettings.CurrentHits == 0f)
                _stateMachine.Enter<Dead>();
        }
    }
}