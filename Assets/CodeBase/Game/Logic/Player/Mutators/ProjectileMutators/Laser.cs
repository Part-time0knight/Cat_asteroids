using Game.Logic.Handlers;
using Game.Logic.Services.Mutators;
using Game.Logic.StaticData.MutatorsData;
using System;
using Zenject;

namespace Game.Logic.Player.Mutators.ProjectileMutators
{
    public class Laser : IInitializable, IDisposable
    {
        public const Mutator Id = Mutator.Laser;

        private readonly Projectiles.Laser.Pool _pool;
        private readonly ProjectileManager _pManager;
        private readonly IMutatorData _mutatorData;
        private readonly IMutatorsObservable _mutatorObservable;
        private readonly BaseProjectile _baseMutator;

        public Laser(Projectiles.Laser.Pool pool,
            ProjectileManager projectileManager,
            IMutatorsObservable mutatorObservable,
            IMutatorData mutatorData,
            BaseProjectile baseMutator) 
        {
            _pool = pool;
            _pManager = projectileManager;
            _mutatorObservable = mutatorObservable;
            _mutatorData = mutatorData;
            _baseMutator = baseMutator;
        }

        public void Initialize()
        {
            _mutatorObservable.OnMutatorUpdate += InvokeUpdate;
        }

        public void Dispose()
        {
            _mutatorObservable.OnMutatorUpdate -= InvokeUpdate;
        }

        private void InvokeUpdate(int id)
        {
            if (id != (int)Id) return;

            bool active = _mutatorData.IsActive(id);
            if (active)
                SetLaser();
            else
                RemoveLaser();
        }

        private void SetLaser()
        {
            _pManager.Pool = _pool;
        }

        private void RemoveLaser()
        {
            _baseMutator.Set();
        }
    }
}