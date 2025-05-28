using Game.Logic.Player;
using UnityEngine;
using Zenject;

namespace Game.Test
{
    public class HealDamage : MonoBehaviour
    {
        private PlayerFacade _player;

        [Inject]
        private void Construct(PlayerFacade player)
        {
            _player = player;
        }

        public void Heal()
        {
            _player.TakeDamage(-1);
        }
    }
}