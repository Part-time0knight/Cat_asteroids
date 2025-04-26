using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Logic.Player;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class TestingToolsViewModel : AbstractViewModel
    {
        private readonly PlayerHandler _playerHandler;

        protected override Type Window => typeof(TestingToolsView);

        public TestingToolsViewModel(IWindowFsm windowFsm, PlayerHandler playerHandler) : base(windowFsm)
        {
            _playerHandler = playerHandler;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        public void MakeDamage()
        {
            _playerHandler.TakeDamage(1);
        }

        public void HealDamage()
        {
            _playerHandler.TakeDamage(-1);
        }
    }
}