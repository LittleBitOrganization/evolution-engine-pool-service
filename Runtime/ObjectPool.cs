using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleBit.Modules.Pool
{
    public class ObjectPool
    {
        private readonly ICreatorPoolObject _creatorPoolObject;
        private readonly GameObject _template;
        private readonly string _name;
        private readonly int _defaultSize;
        private Transform _objectParent;
        private readonly List<GameObject> _pooledObjects;
        private readonly Dictionary<int, GameObject> _aliveObjects;
        private bool Initialized { get; set; }
        private bool HasPooledObjects => _pooledObjects.Count > 0;

        private Transform ObjectParent
        {
            get
            {
                if (!_objectParent)
                {
                    _objectParent = _creatorPoolObject.CreateEmptyGameObject($"{_name} Pool").transform;
                }

                return _objectParent;
            }
        }

        internal ObjectPool(ICreatorPoolObject creatorPoolObject, GameObject template, string name, int defaultSize = 0)
        {
            _creatorPoolObject = creatorPoolObject;
            _template = template;
            _name = name;
            _defaultSize = defaultSize;
            _objectParent = null;

            _pooledObjects = new List<GameObject>();
            _aliveObjects = new Dictionary<int, GameObject>();
        }

        internal void TryInitialize()
        {
            if (Initialized == false)
            {
                Initialized = true;
                CreatePopulation();
            }
            else throw new Exception("The pool has already been initialized ");
        }

        private void CreatePopulation()
        {
            for (int i = 0; i < _defaultSize; i++)
            {
                _pooledObjects.Add(CreateNewObject());
            }
        }

        private GameObject CreateNewObject()
        {
            GameObject poolableInstance = _creatorPoolObject.InstantiatePrefab(_template);
            poolableInstance.name = _name + " (Pooled)";
            return poolableInstance;
        }

        public GameObject GetGameObject()
        {
            GameObject obj;
            if (HasPooledObjects)
            {
                obj = _pooledObjects[_pooledObjects.Count - 1];
                _pooledObjects.RemoveAt(_pooledObjects.Count - 1);

                if (obj == null)
                {
                    throw new Exception(
                        $"Object in pool '{_name}' was null or destroyed; it may have been destroyed externally. Attempting to retrieve a new object");
                }
            }
            else
            {
                obj = CreateNewObject();
            }

            obj.SetActive(true);

            _aliveObjects.Add(obj.GetInstanceID(), obj);
            return obj;
        }

        public void Release(GameObject obj)
        {
            if (obj == null) throw new Exception("Object is null");

            if (!_aliveObjects.Remove(obj.GetInstanceID()))
            {
                throw new Exception(
                    $"Object '{obj}' could not be found in pool '{_name}'; it may have already been released.");
            }
            
            
            _pooledObjects.Add(obj);
            obj.SetActive(false);
            obj.transform.SetParent(ObjectParent, false);
        }
    }
}