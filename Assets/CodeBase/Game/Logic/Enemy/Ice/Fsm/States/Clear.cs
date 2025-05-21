using Core.Infrastructure.GameFsm.States;

namespace Game.Logic.Enemy.Ice.Fsm.States
{
    public class Clear : IState
    {
        private IceShootHandler _shootHandler;

        public Clear(IceShootHandler iceShootHandler)
        {
            _shootHandler = iceShootHandler;
        }

        public void OnEnter()
        {
            _shootHandler.Clear();
        }

        public void OnExit()
        {
        }
    }
}