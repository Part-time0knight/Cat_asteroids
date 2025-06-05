using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Domain.Dto;
using Game.Infrastructure.States.Gameplay;
using Game.Logic.Handlers;
using Game.Logic.Player;
using Game.Logic.Services.Mutators;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{

    public class PowerUpViewModel : AbstractViewModel
    {
        public event Action<PowerUpDto> OnOpen;
        public event Action<ShopDto> OnShopUpdate;

        private readonly IPlayerHitsReader _hitsReader;
        private readonly IPlayerScoreReader _scoreReader;
        private readonly IGameStateMachine _gameFsm;
        private readonly IMutatorData _mutatorData;
        private readonly DifficultHandler _difficultHandler;
        private readonly BundleService _bundleService;
        private readonly PowerUpDto _dto = new();
        private readonly ShopDto _shopDto = new();

        protected override Type Window => typeof(PowerUpView);

        public PowerUpViewModel(IWindowFsm windowFsm,
            IGameStateMachine gameFsm,
            IPlayerScoreReader scoreReader,
            IPlayerHitsReader hitsReader,
            DifficultHandler difficultHandler,
            IMutatorData mutatorData,
            BundleService bundleService) : base(windowFsm)
        {
            _hitsReader = hitsReader;
            _scoreReader = scoreReader;
            _difficultHandler = difficultHandler;
            _gameFsm = gameFsm;
            _mutatorData = mutatorData;
            _bundleService = bundleService;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        public void InvokeContinue()
        {
            _gameFsm.Enter<GameplayState>();
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            if (uiWindow != Window) return;
            Update();

            _bundleService.OnBundleUpdate += UpdateShop;
            UpdateShop();
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            if (uiWindow != Window) return;
            _bundleService.OnBundleUpdate -= UpdateShop;
        }

        private void Update()
        {
            _dto.Hits = _hitsReader.Hits;
            _dto.Score = _scoreReader.Score.ToString();
            _dto.ScoreToNextLayer = _difficultHandler.NextStep.ToString();
            _dto.Layer = "#" + (_difficultHandler.CurrentDifficult - 1);
            OnOpen?
                .Invoke(_dto);
        }

        private void UpdateShop()
        {
            _shopDto.Bundles.Clear();
            var bundlesData = _bundleService.AvailableBundles;

            foreach (var bundle in bundlesData)
            {
                BundleDto newBundle = new()
                {
                    TopName = _mutatorData.GetName(bundle.PlayerId),
                    TopDescription = _mutatorData.GetDescription(bundle.PlayerId),
                    TopIcon = _mutatorData.GetSprite(bundle.PlayerId),

                    BottomName = _mutatorData.GetName(bundle.EnemyId),
                    BottomDescription = _mutatorData.GetDescription(bundle.EnemyId),
                    BottomIcon = _mutatorData.GetSprite(bundle.EnemyId),

                    Cost = bundle.Cost.ToString(),
                    OnBuy = () => InvokeOpenChooseWindow(bundle.Id),
                };

                _shopDto.Bundles.Add(newBundle);
            }

            OnShopUpdate?.Invoke(_shopDto);
        }

        private void InvokeOpenChooseWindow(int bundleId)
        {
            _bundleService.SelectedBundle = bundleId;
            _windowFsm.OpenWindow(typeof(BundleChooseView), false);
        }


    }
}