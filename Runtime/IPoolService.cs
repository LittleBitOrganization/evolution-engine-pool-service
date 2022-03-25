using UnityEngine;

namespace LittleBit.Modules.Pool
{
    public interface IPoolService
    {
        public ObjectPool CreateAndInitialize(GameObject template, string name, int defaultSize = 0);
    }

    public class PoolService : IPoolService
    {
        public ObjectPool CreateAndInitialize(GameObject template, string name, int defaultSize = 0)
        {
            return new ObjectPool(template, name, defaultSize);
        }
    }

    public class ObjectPool
    {
        private readonly GameObject _template;
        private readonly string _name;
        private readonly int _defaultSize;

        internal ObjectPool(GameObject template, string name, int defaultSize = 0)
        {
            _template = template;
            _name = name;
            _defaultSize = defaultSize;
        }
        
    }
}

