
using Game.Logic.Handlers;
using Game.Logic.Projectiles;

namespace Game.Logic.Player.Mutators.ProjectileMutators
{
    public class BaseProjectile
    {
        private readonly ProjectileManager _projectileManager;
        private readonly Bullet.Pool _bulletPool;

        public BaseProjectile(ProjectileManager projectileManager,
            Bullet.Pool pool)
        {
            _projectileManager = projectileManager;
            _bulletPool = pool;
        }

        public void Set()
        {
            _projectileManager.Pool = _bulletPool;
        }
    }
}