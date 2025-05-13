using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Logic.Handlers
{
    public class LoadHandler
    {
        public event Action<float> OnLoad;
        private AsyncOperation _loadingOperation = null;

        public void LoadGamePlay()
        {
            _loadingOperation = SceneManager.LoadSceneAsync("Gameplay");
            _loadingOperation.ObserveEveryValueChanged(op => op.progress)
            .Subscribe(InvokeLoadUpdate);
        }

        private void InvokeLoadUpdate(float progress)
            => OnLoad?.Invoke(progress);
    }
}