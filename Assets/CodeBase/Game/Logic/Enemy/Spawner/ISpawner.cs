using System;
using UnityEngine;

namespace Game.Logic.Enemy.Spawner
{
    public interface ISpawner
    {
        bool Active { get; }

        void Start();
        void Pause();
        void Continue();
        void Stop();
        void Clear();

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Vector2 HorizontalBorders { get; private set; }
            [field: SerializeField] public Vector2 VerticalBorders { get; private set; }
            [field: SerializeField] public float MinimalRangeToPlayer { get; private set; }
            [field: SerializeField] public float Delay { get; private set; }
        }
    }    
}