using UnityEngine;

namespace Game.Logic.Services.Mutators
{
    public interface IMutatorData
    {
        bool IsActive(int mutatorId);
        Sprite GetSprite(int mutatorId);
        string GetName(int mutatorId);
        string GetDescription(int mutatorId);
    }
}