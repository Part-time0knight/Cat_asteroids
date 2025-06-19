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

        void Pause();

        void Continue();

        void SetShield(int hits);
    }
}