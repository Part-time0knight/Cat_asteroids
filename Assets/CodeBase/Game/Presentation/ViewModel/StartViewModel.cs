using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class StartViewModel : AbstractViewModel
    {
        public event Action OnOpen;
        public event Action OnClose;

        protected override Type Window => typeof(StartView);

        public StartViewModel(IWindowFsm windowFsm) : base(windowFsm)
        {
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
            if (Window != uiWindow)
                return;
            OnOpen.Invoke();
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            if (Window != uiWindow)
                return;
            OnClose.Invoke();
        }
    }
}