using UnityEngine;

namespace Game.Logic.Handlers
{
    public abstract class UnitHandler: MonoBehaviour
    {
        protected Settings _settings;

        public int Score => _settings.Score;

        public abstract void MakeCollizion(int damage);

        protected virtual void SetSettings(Settings settings)
        {
            _settings = settings;
        }

        public class Settings
        {
            [field: SerializeField] public int Score {  get; private set; }
        }
    }
}