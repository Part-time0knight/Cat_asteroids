
using System;

namespace Game.Logic.Player.Mutators.VampiricMutator
{
    public class VampiricData : IVampiricReader, IVampiricWriter
    {
        public event Action OnUpdate;

        private int _needPoints;
        private int _currentPoints;

        private bool _reload;

        public int NeedPoints 
        { 
            get => _needPoints;
            set 
            { 
                _needPoints = value;
                OnUpdate?.Invoke();
            }
        }

        public int CurrentPoints
        {
            get => _currentPoints;
            set
            {
                _currentPoints = value;
                OnUpdate?.Invoke();
            }
        }

        public bool Reload
        {
            get => _reload;
            set
            {
                _reload = value;
                OnUpdate?.Invoke();
            }
        }

    }
}