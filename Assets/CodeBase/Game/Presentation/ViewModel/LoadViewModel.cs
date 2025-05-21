using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Logic.Handlers;
using Game.Presentation.View;
using System;
using UnityEngine;

namespace Game.Presentation.ViewModel
{
    public class LoadViewModel : AbstractViewModel
    {
        public event Action<float> OnLoad;

        private readonly SceneLoadHandler _loadHandler;

        protected override Type Window => typeof(LoadView);

        public LoadViewModel(IWindowFsm windowFsm,
            SceneLoadHandler loadHandler) : base(windowFsm)
        {
            _loadHandler = loadHandler;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            if (uiWindow != Window) return;
            _loadHandler.OnLoad += UpdateLoad;
            OnLoad.Invoke(0f);
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            if (uiWindow != Window) return;
            _loadHandler.OnLoad -= UpdateLoad;
        }

        private void UpdateLoad(float progress)
        {
            Debug.Log("Progress = " + progress);
            OnLoad?.Invoke(Mathf.Clamp01(progress / 0.9f));
        }
    }
}