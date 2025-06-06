using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Logic.Services.Mutators
{
    public class BundleService : IInitializable
    {
        public event Action OnBundleUpdate;

        private readonly IMutatorGetter _mutatorGetter;
        private readonly IMutatorSetter _mutatorSetter;
        private readonly Settings _settings;
        private readonly List<Bundle> _availableBundles = new(3);
        private readonly List<Bundle> _slots = new(3);

        private int _selectedBundle = -1;
        private int _selectedSlot = -1;

        public List<Bundle> AvailableBundles => new(_availableBundles);
        public List<Bundle> Slots => new(_slots);

        public int SelectedBundle 
        { 
            get => _selectedBundle;
            set => _selectedBundle = value;
        }

        public int SelectedSlot
        {
            get => _selectedSlot; 
            set
            {
                _selectedSlot = value;
                BuyBundle();
            }
        }


        public BundleService(IMutatorGetter mutatorGetter,
            IMutatorSetter setter,
            Settings settings)
        {
            _mutatorGetter = mutatorGetter;
            _mutatorSetter = setter;
            _settings = settings;
        }

        public Bundle GetSlot(int index)
            => _slots[index];

        public void GenerateBundles()
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

        public void Initialize()
        {
            for (int i = 0; i < _settings.BundleCount; i++)
            {
                Bundle bundle = new()
                {
                    Id = i,
                    PlayerId = -1,
                    EnemyId = -1,
                    Cost = 0
                };
                _slots.Add(bundle);
            }
        }

        public void BuyBundle()
        {
            var bundle = ActivateBundle(SelectedBundle);
            RemoveSlot(SelectedSlot);

            _availableBundles.Remove(bundle);

            bundle.Id = SelectedSlot;

            _slots[SelectedSlot] = bundle;

            OnBundleUpdate?.Invoke();
        }

        private Bundle ActivateBundle(int id)
        {
            var bundle = _availableBundles
                .FirstOrDefault(i => i.Id == id);

            _mutatorSetter.SetActive(bundle.PlayerId, true);
            _mutatorSetter.SetActive(bundle.EnemyId, true);

            return bundle;
        }

        private void RemoveSlot(int pos)
        {
            if (_slots[pos].PlayerId == -1)
                return;

            _mutatorSetter.SetActive(_slots[pos].PlayerId, false);
            _mutatorSetter.SetActive(_slots[pos].EnemyId, false);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public int ConstantCost { get; private set; }
            [field: SerializeField] public Vector2Int RandomRangeCost { get; private set; }

            [field: SerializeField] public int BundleCount { get; private set; }
            [field: SerializeField] public int SlotsCount { get; private set; }
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