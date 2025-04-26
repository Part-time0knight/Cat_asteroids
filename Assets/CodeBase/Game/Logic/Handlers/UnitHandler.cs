using UnityEngine;

namespace Game.Logic.Handlers
{
    public abstract class UnitHandler : MonoBehaviour
    {
        public abstract void MakeCollizion(int damage);
    }
}