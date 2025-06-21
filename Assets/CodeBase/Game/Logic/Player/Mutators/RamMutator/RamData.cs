using System;
using UnityEngine;

namespace Game.Logic.Player.Mutators.RamMutator
{
    public class RamData : IRamReader, IRamWriter
    {
        public event Action OnActivate;
        public event Action OnReloadEnd;
        public event Action<bool> OnPause;

        private bool _pause;
        private bool _reload;

        public float ReloadTime { get; set; }

        public bool Reload 
        {
            get => _reload; 
            set
            {
                _reload = value;
                if (value)
                    OnActivate();
                else
                    OnReloadEnd();
            }
        }

        public bool Pause
        {
            get => _pause;
            set
            {
                _pause = value;
                OnPause?.Invoke(value);
            }
        }
    }
}