using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Handlers;
using Game.Presentation.View;

namespace Game.Infrastructure.States.Gameplay
{
    public class LoadMenu : IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly SceneLoadHandler _loadHandler;

        public LoadMenu(IWindowFsm windowFsm,
            SceneLoadHandler loadHandler)
        {
            _windowFsm = windowFsm;
            _loadHandler = loadHandler;
        }

        public void OnEnter()
        {
            UnityEngine.Debug.Log("Enter state Load Menu");
            _loadHandler.LoadMenu();
            _windowFsm.OpenWindow(typeof(LoadView), true);
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow();
        }
    }
}