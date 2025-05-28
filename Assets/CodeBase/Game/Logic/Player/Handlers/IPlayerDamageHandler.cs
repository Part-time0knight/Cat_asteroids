using Game.Logic.Handlers;

namespace Game.Logic.Player.Handlers
{
    public interface IPlayerDamageHandler : IDamageHandler
    {
        /// <summary>
        /// Power reduce damage to 0
        /// </summary>
        bool Power { get; set; }

        void Reset();

        public void Pause();

        public void Continue();
    }
}