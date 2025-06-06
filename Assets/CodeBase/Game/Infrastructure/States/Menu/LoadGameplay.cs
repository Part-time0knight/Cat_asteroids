using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Handlers;
using Game.Presentation.View;

namespace Game.Infrastructure.States.Menu
{
    public class LoadGameplay : IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly SceneLoadHandler _loadHandler;

        public LoadGameplay(IWindowFsm windowFsm,
            SceneLoadHandler loadHandler)
        {
            _windowFsm = windowFsm;
            _loadHandler = loadHandler;
        }

        public void OnEnter()
        {
            _loadHandler.LoadGameplay();
            _windowFsm.OpenWindow(typeof(LoadView), true);
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow();
        }
    }
}