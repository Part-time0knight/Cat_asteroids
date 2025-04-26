using Game.Presentation.View;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using System;
using Game.Logic.Player;
using Game.Logic.Handlers;

namespace Game.Presentation.ViewModel
{
    public class PlayerViewModel : AbstractViewModel, IPauseble
    {
        public event Action<float> InvokeReloadActive;
        public event Action<float> InvokeHitsUpdate;
        public event Action<bool> InvokePause;

        protected override Type Window => typeof(PlayerView);

        private readonly PlayerShootHandler.PlayerSettings _playerSettings;
        private readonly PlayerDamageHandler.PlayerSettings _hitsSettings;
        private readonly IPauseHandler _pauseHandler;

        public PlayerViewModel(IWindowFsm windowFsm, PlayerDamageHandler.PlayerSettings hitsSettings,
            PlayerShootHandler.PlayerSettings shootSettings, IPauseHandler pauseHandler) : base(windowFsm)
        {
            _playerSettings = shootSettings;
            _hitsSettings = hitsSettings;
            _pauseHandler = pauseHandler;
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            _playerSettings.InvokeShot += ReloadUpdate;
            _hitsSettings.InvokeHitPointsChange += HitPointsUpdate;
            _pauseHandler.SubscribeElement(this);
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            _playerSettings.InvokeShot -= ReloadUpdate;
            _hitsSettings.InvokeHitPointsChange -= HitPointsUpdate;
            _pauseHandler.UnsubscribeElement(this);
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
            InvokeHitsUpdate?.Invoke((float)_hitsSettings.CurrentHits/_hitsSettings.HitPoints);
        }

        private void ReloadUpdate()
        {
            InvokeReloadActive?.Invoke(_playerSettings.CurrentAttackDelay);
        }

        public void OnPause(bool active)
        {
            InvokePause?.Invoke(active);
        }
    }
}