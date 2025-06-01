
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.StaticData.MutatorsData
{
    [CreateAssetMenu(fileName = "MutatorsListSO", menuName = "Data/MutatorsListSO")]
    public class MutatorsListSO : ScriptableObject
    {
        [field: SerializeField] public List<MutatorSO> Mutators { get; private set; }
    }
}