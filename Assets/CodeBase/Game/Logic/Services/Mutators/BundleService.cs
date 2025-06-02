using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Logic.Services.Mutators
{
    public class BundleService
    {
        public event Action OnBundleUpdate;

        private readonly IMutatorGetter _mutatorGetter;
        private readonly IMutatorSetter _mutatorSetter;
        private readonly Settings _settings;
        private readonly List<Bundle> _availableBundles = new();
        private readonly List<Bundle> _activeBundles = new();

        public List<Bundle> AvailableBundles => new(_availableBundles);

        public BundleService(IMutatorGetter mutatorGetter,
            IMutatorSetter setter,
            Settings settings)
        {
            _mutatorGetter = mutatorGetter;
            _mutatorSetter = setter;
            _settings = settings;
        }

        public void GenerateBundle()
        {
            _availableBundles.Clear();
            var pMutators = _mutatorGetter.AvailablePlayerMutators;
            var eMutators = _mutatorGetter.AvailableEnemyMutators;
            for (int i = 0; i < _settings.BundleCount; i++)
            {
                Bundle bundle = new()
                {
                    Id = i,
                    PlayerId = pMutators[Random.Range(0, pMutators.Count)],
                    EnemyId = eMutators[Random.Range(0, eMutators.Count)],
                    Cost = _settings.ConstantCost
                        + Random.Range(_settings.RandomRangeCost.x,
                            _settings.RandomRangeCost.y)
                };
                _availableBundles.Add(bundle);
            }

            OnBundleUpdate?.Invoke();
        }

        public void BuyBundle(int id, int pos)
        {
            BuyBundle(id);
        }

        public void BuyBundle(int id)
        {
            var bundle = _availableBundles.FirstOrDefault(i => i.Id == id);
            _availableBundles.Remove(bundle);
            _mutatorSetter.SetActive(bundle.PlayerId);
            _mutatorSetter.SetActive(bundle.EnemyId);
            OnBundleUpdate?.Invoke();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public int ConstantCost { get; private set; }
            [field: SerializeField] public Vector2Int RandomRangeCost { get; private set; }

            [field: SerializeField] public int BundleCount { get; private set; }
        }

        public struct Bundle
        {
            public int Id;
            public int PlayerId;
            public int EnemyId;
            public int Cost;
        }
    }
}