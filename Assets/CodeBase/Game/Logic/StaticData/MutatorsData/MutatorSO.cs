using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.StaticData.MutatorsData
{
    [CreateAssetMenu(fileName = "MutatorSO", menuName = "Data/MutatorSO")]
    public class MutatorSO : ScriptableObject
    {
        [field: SerializeField] public Mutator Id { get; private set; }
        [field: SerializeField] public MutatorType Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public List<Mutator> Dependency { get; private set; } = new();
    }
}