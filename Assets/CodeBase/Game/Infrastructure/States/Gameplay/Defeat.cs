using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;

namespace Game.Infrastructure.States.Gameplay
{
    public class Defeat : IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly IScoreSave _scoreSave;

        public Defeat(IWindowFsm windowFsm, IScoreSave scoreSave)
        {
            _windowFsm = windowFsm;
            _scoreSave = scoreSave;
        }

        public void OnEnter()
        {
            _scoreSave.Save();
            _windowFsm.OpenWindow(typeof(DefeatView), true);
            UnityEngine.Debug.Log("Enter state Defeat");
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow();
        }
    }
}