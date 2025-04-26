using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player
{
    public class PlayerDamageHandler : DamageHandler
    {
        public PlayerDamageHandler(PlayerSettings stats, IPauseHandler pauseHandler) : base(stats, pauseHandler)
        { }

        [Serializable]
        public class PlayerSettings : Settings
        { }
    }
}