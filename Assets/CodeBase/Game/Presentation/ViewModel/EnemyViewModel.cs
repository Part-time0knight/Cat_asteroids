using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Logic.Enemy;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class EnemyViewModel : AbstractViewModel
    {
        public event Action<float> InvokeHitsUpdate;

        private readonly EnemyDamageHandler.EnemySettings _hitsSettings;

        protected override Type Window => typeof(EnemyView);

        public EnemyViewModel(IWindowFsm windowFsm,
            EnemyDamageHandler.EnemySettings settings) : base(windowFsm)
        {
            _hitsSettings = settings;
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            HitPointsUpdate();
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow(Window);
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: false);
        }

        private void HitPointsUpdate()
        {
            
        }
    }
}