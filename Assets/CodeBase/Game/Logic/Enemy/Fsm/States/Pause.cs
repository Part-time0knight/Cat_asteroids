using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Logic.Enemy.Fsm.States
{
    public class Pause : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly EnemyHandler _enemy;

        public Pause(IGameStateMachine stateMachine,
            EnemyHandler enemy)
        {
            _enemy = enemy;
            _stateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
            _enemy.OnPause += InvokePause;
        }

        public virtual void OnExit()
        {
            _enemy.OnPause -= InvokePause;
        }

        private void InvokePause(bool pause)
        {
            if (pause) return;
            _stateMachine.Enter<Run>();
        }
    }
}