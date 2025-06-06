using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Player.Mutators.ProjectileMutators;
using Game.Logic.Player.Mutators.ShooterMutators;

namespace Game.Logic.Player.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly BaseProjectile _baseLaserMutator;
        private readonly BaseShooter _baseShooter;

        public Initialize(IGameStateMachine stateMachine,
            BaseProjectile baseLaserMutator,
            BaseShooter baseShooter)
        {
            _stateMachine = stateMachine;
            _baseLaserMutator = baseLaserMutator;
            _baseShooter = baseShooter;
        }

        public void OnEnter()
        {
            _baseLaserMutator.Set();
            _baseShooter.Set();
            _stateMachine.Enter<Idle>();
        }

        public void OnExit()
        {
            
        }
    }
}