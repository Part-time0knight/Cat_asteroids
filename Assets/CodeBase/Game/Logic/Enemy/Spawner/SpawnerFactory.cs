using Zenject;

namespace Game.Logic.Enemy.Spawner
{
    public class SpawnerFactory : PlaceholderFactory<EnemyHandler.Pool, ISpawner.Settings, ISpawner>
    {
    }
}