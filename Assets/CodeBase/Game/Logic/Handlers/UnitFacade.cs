using UnityEngine;

namespace Game.Logic.Handlers
{
    public abstract class UnitFacade : MonoBehaviour
    {
        protected Settings _settings;

        public int Score
        {
            get
            {
                if (_settings == null)
                    return 0;

                return _settings.Score;
            }
        }
        public float Size => _settings.SizeMultiplier;

        public virtual bool Pause { get; set; }

        public abstract void MakeCollision(int damage);

        protected virtual void SetSettings(Settings settings)
        {
            _settings = settings;
        }

        public class Settings
        {
            [field: SerializeField] public int Score {  get; private set; }
            [field: SerializeField] public float SizeMultiplier { get; private set; }
        }
    }
}