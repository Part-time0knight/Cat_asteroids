using Game.Logic.Handlers;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;

namespace Game.Logic.Player.Mutators.ProjectileMutators
{
    public class Laser : AbstractMutator
    {
        private readonly Projectiles.Laser.Pool _pool;
        private readonly ProjectileManager _pManager;
        private readonly BaseProjectile _baseMutator;

        protected override Mutator Id => Mutator.Laser;

        public Laser(Projectiles.Laser.Pool pool,
            ProjectileManager projectileManager,
            IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData,
            BaseProjectile baseMutator) :base(mutatorObservable, mutatorData)
        {
            _pool = pool;
            _pManager = projectileManager;
            _baseMutator = baseMutator;
        }

        protected override void Set()
        {
            _pManager.Pool = _pool;
        }

        protected override void Remove()
        {
            _baseMutator.Set();
        }
    }
}