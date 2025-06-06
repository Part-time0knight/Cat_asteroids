using System;

namespace Game.Logic.Player
{
    public interface IBurstReader
    {
        public event Action OnAmmoChange;
        public event Action OnTimeChange;

        public int Ammo { get; }

        public float ReloadTime { get; }
    }
}