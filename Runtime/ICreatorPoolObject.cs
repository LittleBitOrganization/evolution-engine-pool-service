using UnityEngine;
using Object = System.Object;

namespace LittleBit.Modules.Pool
{
    public interface ICreatorPoolObject
    {
        public GameObject InstantiatePrefab(Object prefab);
        public GameObject CreateEmptyGameObject(string name);
    }
}