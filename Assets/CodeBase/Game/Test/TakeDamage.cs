using Game.Logic.Player;
using UnityEngine;
using Zenject;

namespace Game.Test
{
    public class TakeDamage : MonoBehaviour
    {
        private PlayerFacade _player;

        [Inject]
        private void Construct(PlayerFacade player)
        {
            _player = player;
        }

        public void MakeDamage()
        {
            _player.TakeDamage(1);
        }
    }
}