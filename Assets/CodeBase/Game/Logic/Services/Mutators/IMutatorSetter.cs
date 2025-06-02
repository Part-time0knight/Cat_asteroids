using UnityEngine;

namespace Game.Logic.Services.Mutators
{
    public interface IMutatorSetter
    {
        void SetActive(int mutatorId);
    }
}