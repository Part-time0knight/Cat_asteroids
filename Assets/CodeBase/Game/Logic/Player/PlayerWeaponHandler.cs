using Game.Logic.Handlers;
using System;

namespace Game.Logic.Player
{
    public class PlayerWeaponHandler : WeaponHandler
    {
        public PlayerWeaponHandler(PlayerSettings settings) : base(settings)
        {
        }

        [Serializable]
        public class PlayerSettings : Settings
        {

        }
    }
}