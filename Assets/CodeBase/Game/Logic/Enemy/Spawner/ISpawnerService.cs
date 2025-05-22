
namespace Game.Logic.Enemy.Spawner
{
    public interface ISpawnerService
    {
        void Start(string id);

        void Stop(string id);

        void Clear(string id);

        void ClearAll();

        void KillAll();

        void PauseAll();

        void ContinueAll();
    }
}