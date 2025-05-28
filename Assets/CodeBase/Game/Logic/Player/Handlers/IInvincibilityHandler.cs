
using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player.Handlers
{
    public interface IInvincibilityHandler : IHandler
    {
        event Action<bool> OnPowerChange;

        void Start();

        public void Pause();

        public void Continue();

        void Stop();
    }
}