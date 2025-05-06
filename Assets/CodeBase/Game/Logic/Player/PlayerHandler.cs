using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers;
using Game.Logic.Player.Fsm.States;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerHandler : UnitHandler
    {
        public event Action<int> OnTakeDamage;
        public event Action<bool> OnActiveShootChange;
        public event Action<bool> OnPause;

        private IGameStateMachine _playerFSM;

        private bool _activeShooting = false;
        private bool _pause = false;

        public bool ActiveShooting 
        { 
            get => _activeShooting; 
            set
            {
                _activeShooting = value;
                OnActiveShootChange?.Invoke(value);
            } 
        }

        public override bool Pause
        {
            get => _pause;
            set
            {
                _pause = value;
                OnPause?.Invoke(value);
            }
        }

        public override void MakeCollizion(int damage)
            => TakeDamage(damage);

        public void TakeDamage(int damage)
        {
            OnTakeDamage?.Invoke(damage);
        }

        public void ResetPlayer()
        {
            _playerFSM.Enter<Initialize>();
            transform.position = Vector2.zero;
            transform.eulerAngles = Vector3.zero;
        }

        [Inject]
        private void Construct(IGameStateMachine playerFSM)
        {
            _playerFSM = playerFSM;
        }

        [Inject]
        private void SetPlayerSettings(PlayerSettings settings)
            => SetSettings(settings);



        [Serializable]
        public class PlayerSettings : Settings
        {
        }

        public class Pool : MemoryPool<PlayerHandler>
        {
            protected override void Reinitialize(PlayerHandler item)
            {
                base.Reinitialize(item);
            }
        }
    }
}