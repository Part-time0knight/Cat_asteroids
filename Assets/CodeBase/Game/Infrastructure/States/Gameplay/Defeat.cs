using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;

namespace Game.Infrastructure.States.Gameplay
{
    public class Defeat : IState
    {
        private readonly IWindowFsm _windowFsm;

        public Defeat(IWindowFsm windowFsm)
        {
            _windowFsm = windowFsm;
        }

        public void OnEnter()
        {
            _windowFsm.OpenWindow(typeof(DefeatView), true);
            UnityEngine.Debug.Log("Enter state Defeat");
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow();
        }
    }
}