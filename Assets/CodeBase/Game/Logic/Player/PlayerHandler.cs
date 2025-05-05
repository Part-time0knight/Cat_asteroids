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

        private IGameStateMachine _playerFSM;

        public override void MakeCollizion(int damage)
            => TakeDamage(damage);

        public void TakeDamage(int damage)
        {
            OnTakeDamage?.Invoke(damage);
        }

        [Inject]
        private void Construct(IGameStateMachine playerFSM)
        {
            _playerFSM = playerFSM;
        }

        [Inject]
        private void SetPlayerSettings(PlayerSettings settings)
            => SetSettings(settings);

        private void ResetPlayer()
        {
            _playerFSM.Enter<Initialize>();
            transform.position = Vector2.zero;
            transform.eulerAngles = Vector3.zero;
        }

        [Serializable]
        public class PlayerSettings : Settings
        {
        }

        public class Pool : MemoryPool<PlayerHandler>
        {
            protected override void Reinitialize(PlayerHandler item)
            {
                base.Reinitialize(item);
                item.ResetPlayer();
            }
        }
    }
}