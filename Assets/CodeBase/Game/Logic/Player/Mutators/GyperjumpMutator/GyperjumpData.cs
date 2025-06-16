using System;
using UnityEngine;

namespace Game.Logic.Player.Mutators.GyperjumpMutator
{
    public class GyperjumpData : IGyperjumpReader, IGyperjumpWriter
    {
        public event Action OnActivate;
        public event Action OnReloadEnd;
        public event Action<bool> OnPause;

        private bool _pause;

        public float ReloadTime { get; set; }

        public bool Reload { get; set; }

        public bool Pause
        { 
            get => _pause;
            set
            {
                _pause = value;
                OnPause?.Invoke(value);
            }
        }

        public void SetReload()
            => OnActivate?.Invoke();

        public void SetEndReload()
            => OnReloadEnd?.Invoke();
    }
}