using System;

namespace Game.Logic.Player.Mutators.ShooterMutators
{
    public interface IBurstReader
    {
        event Action OnAmmoChange;
        event Action OnTimeChange;

        int Ammo { get; }

        float ReloadTime { get; }
    }
}