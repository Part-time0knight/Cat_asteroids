using Game.Logic.Player;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<PlayerScoreService>()
            .AsSingle()
            .NonLazy();
    }

}