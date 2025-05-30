using Zenject;

namespace Game.Logic.Enemy.Spawner
{
    public class SpawnerFactory : PlaceholderFactory<EnemyFacade.Pool, ISpawner.Settings, ISpawner>
    {
    }
}