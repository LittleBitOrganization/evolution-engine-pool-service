using UnityEngine;

namespace LittleBit.Modules.Pool
{
    public interface ICreatorPoolObject
    {
        public GameObject InstantiatePrefab(Object prefab);
        public GameObject CreateEmptyGameObject(string name);
    }
}