using System;

namespace Game.Logic.Player
{
    public class BurstData : IBurstReader, IBurstWritter
    {
        public event Action OnAmmoChange;
        public event Action OnTimeChange;

        private int _ammo;
        private float _reloadTime;

        public int Ammo
        {
            get => _ammo;
            set
            {
                _ammo = value;
                OnAmmoChange?.Invoke();
            }
        }

        public float ReloadTime
        {
            get => _reloadTime;
            set
            {
                _reloadTime = value;
                OnTimeChange?.Invoke();
            }
        }
    }
}