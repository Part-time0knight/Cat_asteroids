using System;

namespace Game.Logic.Services.Mutators
{
    public interface IMutatorsObservable
    {
        event Action<int> OnMutatorUpdate;
        event Action<bool> OnMutatorsPause;
    }
}