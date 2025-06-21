
using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player.Handlers
{
    public interface IInvincibilityHandler : IHandler
    {
        event Action<bool> OnPowerChange;

        void Start();

        void Start(float duration);

        public void Pause();

        public void Continue();

        void Stop();
    }
}