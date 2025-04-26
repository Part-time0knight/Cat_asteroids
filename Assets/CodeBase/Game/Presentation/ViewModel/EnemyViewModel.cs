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
            EnemySettingsHandler settings) : base(windowFsm)
        {
            _hitsSettings = settings.DamageSettings;
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            _hitsSettings.InvokeHitPointsChange += HitPointsUpdate;
            HitPointsUpdate();
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            _hitsSettings.InvokeHitPointsChange -= HitPointsUpdate;
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
            InvokeHitsUpdate?.Invoke((float)_hitsSettings.CurrentHits / _hitsSettings.HitPoints);
        }
    }
}