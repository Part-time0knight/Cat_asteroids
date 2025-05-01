using UnityEngine;
using Zenject;

public class Explosion : MonoBehaviour
{
    

    public class Pool : MonoMemoryPool<Vector2, Explosion>
    {
        protected override void Reinitialize(Vector2 spawnPoint, Explosion item)
        {
            base.Reinitialize(spawnPoint, item);
        }
    }
}
