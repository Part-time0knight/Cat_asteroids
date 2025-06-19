
using System;

namespace Game.Logic.Player.Mutators.VampiricMutator
{
    public interface IVampiricReader
    {
        event Action OnUpdate;

        int NeedPoints { get; }

        int CurrentPoints { get; }

        bool Reload { get; }
    }
}