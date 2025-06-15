using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Player.Mutators.ProjectileMutators;
using Game.Logic.Player.Mutators.ShooterMutators;
using Game.Logic.Player.Mutators.TargetMutators;
using Game.Logic.Services.Mutators;

namespace Game.Logic.Player.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly BaseProjectile _baseLaserMutator;
        private readonly BaseShooter _baseShooter;
        private readonly BaseTarget _baseTarget;
        private readonly BundleInput _bundleInput;

        public Initialize(IGameStateMachine stateMachine,
            BaseProjectile baseLaserMutator,
            BaseShooter baseShooter,
            BaseTarget baseTarget,
            BundleInput bundleInput)
        {
            _stateMachine = stateMachine;
            _baseLaserMutator = baseLaserMutator;
            _baseShooter = baseShooter;
            _baseTarget = baseTarget;
            _bundleInput = bundleInput;
        }

        public void OnEnter()
        {
            _baseLaserMutator.Set();
            _baseShooter.Set();
            _baseTarget.Set();
            _stateMachine.Enter<Idle>();
            _bundleInput.Active = false;
        }

        public void OnExit()
        {
            
        }
    }
}