using Core.Infrastructure.GameFsm;
using Game.Logic.Handlers;
using Game.Logic.Player.Fsm.States;
using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerFacade : UnitFacade
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

        public override void MakeCollision(int damage)
            => TakeDamage(damage);

        public void TakeDamage(int damage)
        {
            OnTakeDamage?.Invoke(damage);
        }

        public void ResetPlayer()
        {
            _playerFSM.Enter<Reset>();
            transform.position = Vector2.zero;
            transform.eulerAngles = Vector3.zero;
        }

        public void InitializePlayer()
        {
            _playerFSM.Enter<Initialize>();
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

        public class Pool : MemoryPool<PlayerFacade>
        {
            protected override void OnCreated(PlayerFacade item)
            {
                base.OnCreated(item);
                item.InitializePlayer();
            }

            protected override void Reinitialize(PlayerFacade item)
            {
                base.Reinitialize(item);
            }
        }
    }
}