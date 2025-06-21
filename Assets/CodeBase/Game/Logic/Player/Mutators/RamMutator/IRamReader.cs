using System;
using UnityEngine;

namespace Game.Logic.Player.Mutators.RamMutator
{
    public interface IRamReader
    {
        event Action OnActivate;
        event Action OnReloadEnd;
        event Action<bool> OnPause;

        float ReloadTime { get; }

        bool Reload { get; }

        bool Pause { get; }
    }
}