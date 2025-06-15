using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Services.Mutators
{
    public class BundleInput : ITickable, IFixedTickable
    {
        public event Action<int> OnButtonDown;

        public event Action<int> OnButton;

        private readonly BundleService _bundleService;
        private bool _active;

        public bool Active 
        {
            get => _active; 
            set => _active = value;
        }

        public BundleInput(BundleService bundleService)
        {
            _bundleService = bundleService;
        }

        public void Tick()
        {
            if (!_active) return;

            if (Input.GetButtonDown("Mutator1"))
                OnButtonDown?.Invoke(_bundleService.GetSlot(0).PlayerId);
            if (Input.GetButtonDown("Mutator2"))
                OnButtonDown?.Invoke(_bundleService.GetSlot(1).PlayerId);
            if (Input.GetButtonDown("Mutator3"))
                OnButtonDown?.Invoke(_bundleService.GetSlot(2).PlayerId);
        }

        public void FixedTick()
        {
            if (!_active) return;

            if (Input.GetButton("Mutator1"))
                OnButton?.Invoke(_bundleService.GetSlot(0).PlayerId);
            if (Input.GetButton("Mutator2"))
                OnButton?.Invoke(_bundleService.GetSlot(1).PlayerId);
            if (Input.GetButton("Mutator3"))
                OnButton?.Invoke(_bundleService.GetSlot(2).PlayerId);
        }
    }
}