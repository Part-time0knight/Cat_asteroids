using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Logic.Player.Mutators.ProjectileMutators;

namespace Game.Logic.Player.Fsm.States
{
    public class Initialize : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly BaseProjectile _baseLaserMutator;

        public Initialize(IGameStateMachine stateMachine,
            BaseProjectile baseLaserMutator)
        {
            _stateMachine = stateMachine;
            _baseLaserMutator = baseLaserMutator;
        }

        public void OnEnter()
        {
            _baseLaserMutator.Set();
            _stateMachine.Enter<Idle>();
        }

        public void OnExit()
        {
            
        }
    }
}