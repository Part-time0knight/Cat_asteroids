using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Logic.Handlers
{
    public class SceneLoadHandler
    {
        public event Action<float> OnLoad;
        private AsyncOperation _loadingOperation = null;

        public void LoadGameplay()
        {
            _loadingOperation = SceneManager.LoadSceneAsync("Gameplay");
            _loadingOperation.ObserveEveryValueChanged(op => op.progress)
            .Subscribe(InvokeLoadUpdate);
        }

        public void LoadMenu()
        {
            _loadingOperation = SceneManager.LoadSceneAsync("Menu");
            _loadingOperation.ObserveEveryValueChanged(op => op.progress)
            .Subscribe(InvokeLoadUpdate);
        }

        private void InvokeLoadUpdate(float progress)
            => OnLoad?.Invoke(progress);
    }
}