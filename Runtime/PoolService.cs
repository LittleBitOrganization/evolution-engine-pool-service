using UnityEngine;

namespace LittleBit.Modules.Pool
{
    public class PoolService : IPoolService
    {
        private readonly ICreatorPoolObject _creatorPoolObject;

        public PoolService(ICreatorPoolObject creatorPoolObject)
        {
            _creatorPoolObject = creatorPoolObject;
        }
        public ObjectPool CreateAndInitialize(GameObject template, string name, int defaultSize = 0)
        {
            ObjectPool objectPool = new ObjectPool(_creatorPoolObject, template, name, defaultSize);
            objectPool.TryInitialize();
            return objectPool;
        }
    }
}