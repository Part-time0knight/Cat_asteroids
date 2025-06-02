using System.Collections.Generic;

namespace Game.Logic.Services.Mutators
{
    public interface IMutatorGetter
    {
        List<int> AvailablePlayerMutators { get; }
        List<int> ActivePlayerMutators { get; }

        List<int> AvailableEnemyMutators { get; }
        List<int> ActiveEnemyMutators { get; }
    }
}