using UnityEngine;

namespace LittleBit.Modules.Pool
{
    public interface IPoolService
    {
        public ObjectPool CreateAndInitialize(GameObject template, string name, int defaultSize = 0);
    }
}

