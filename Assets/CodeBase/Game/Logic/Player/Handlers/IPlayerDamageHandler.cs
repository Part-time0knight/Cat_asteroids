using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player.Handlers
{
    public interface IPlayerDamageHandler : IDamageHandler
    {
        event Action OnTryTakeDamage;

        /// <summary>
        /// Power reduce damage to 0
        /// </summary>
        bool Power { get; set; }

        public void Pause();

        public void Continue();
    }
}