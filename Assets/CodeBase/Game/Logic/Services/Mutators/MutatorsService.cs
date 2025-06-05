using Game.Logic.StaticData.MutatorsData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Logic.Services.Mutators
{
    public class MutatorsService : IInitializable, 
        IMutatorSetter, 
        IMutatorGetter,
        IMutatorsObservable,
        IMutatorData
    {
        public event Action<int> OnMutatorUpdate;

        private readonly List<MutatorSO> _mutatorsSO;

        private readonly Dictionary<int, Mutator> _mutators = new();

        

        public List<int> AvailablePlayerMutators =>
            _mutators.Where(kv => kv.Value.Available && kv.Value.IsPlayer)
                .Select(kv => kv.Key)
                .ToList();

        public List<int> ActivePlayerMutators =>
            _mutators.Where(kv => kv.Value.Active && kv.Value.IsPlayer)
                .Select(kv => kv.Key)
                .ToList();

        public List<int> AvailableEnemyMutators =>
            _mutators.Where(kv => kv.Value.Available && !kv.Value.IsPlayer)
                .Select(kv => kv.Key)
                .ToList();

        public List<int> ActiveEnemyMutators =>
            _mutators.Where(kv => kv.Value.Active && !kv.Value.IsPlayer)
                .Select(kv => kv.Key)
                .ToList();

        public MutatorsService(MutatorsListSO mutators)
        {
            _mutatorsSO = new(mutators.Mutators);
        }

        public void Initialize()
        {
            foreach (var mutator in _mutatorsSO) 
            {
                _mutators.Add((int)mutator.Id, 
                    new()
                    {
                        Id = (int)mutator.Id,
                        IsPlayer = mutator.Type == MutatorType.Player,
                        Active = false,
                        Available = mutator.Dependency.Count == 0,
                        SO = mutator,
                    }
                );
            }
        }

        public void SetActive(int mutatorId, bool active)
        {
            _mutators[mutatorId].Active = active;
            OnMutatorUpdate?.Invoke(mutatorId);
            Update();
        }

        public Sprite GetSprite(int mutatorId)
            => _mutators[mutatorId].SO.Icon;

        public string GetName(int mutatorId)
            => _mutators[mutatorId].SO.Name;

        public string GetDescription(int mutatorId)
            => _mutators[mutatorId].SO.Description;

        public bool IsActive(int mutatorId)
            => _mutators[mutatorId].Active;

        private void Update()
        {
            foreach(var item in _mutators.Values)
            {
                item.Available = IsAvailable(item.Id);
            }
        }

        private bool IsAvailable(int id)
        {
            bool available = true;
            foreach(int dependency in _mutators[id].SO.Dependency)
            {
                if (!_mutators[dependency].Active)
                    return false;
            }
            return available;
        }

        public class Mutator
        {
            public int Id { get; set; }

            public bool Active { get; set; }
            public bool Available { get; set; }
            public bool IsPlayer { get; set; }

            public MutatorSO SO { get; set; }
        }
    }
}