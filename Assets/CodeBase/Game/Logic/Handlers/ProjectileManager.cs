using Game.Logic.Projectiles;
using System;

namespace Game.Logic.Handlers
{
    public class ProjectileManager
    {
        public event Action OnPoolChange;

        private IProjectilePool _pool;

        public IProjectilePool Pool
        {
            get => _pool;
            set
            {
                _pool = value;
                OnPoolChange?.Invoke();
            }
        }
    }
}