using System;

namespace Game.Logic.Player.Mutators.GyperjumpMutator
{
    public interface IGyperjumpReader
    {
        event Action OnActivate;
        event Action OnReloadEnd;
        event Action<bool> OnPause;

        float ReloadTime { get; }

        bool Reload { get; }

        bool Pause { get; }
    }
}