using Core.MVVM.Windows;
using Game.Domain.Factories.GameFsm;
using Game.Infrastructure;
using Game.Logic.Handlers;
using Game.Presentation.ViewModel;
using Zenject;

namespace Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallFactory();
            InstallService();
            InstallViewModel();
        }

        private void InstallFactory()
        {
            Container
                .BindInterfacesAndSelfTo<StatesFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallViewModel()
        {
            Container
                .BindInterfacesAndSelfTo<MainMenuViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<LoadViewModel>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallService()
        {
            Container
                .BindInterfacesAndSelfTo<LoadHandler>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<WindowFsm>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<MenuFsm>()
                .AsSingle()
                .NonLazy();
        }
    }
}