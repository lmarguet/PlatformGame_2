using System.Collections.Generic;
using UnityEngine;

namespace Game.Shared
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly List<T> _availableInstances;
        private readonly List<T> _usedInstances;

        public ObjectPool(T prefab)
        {
            _prefab = prefab;
            _availableInstances = new List<T>();
            _usedInstances = new List<T>();
        }

        public T GetInstance()
        {
            var instance = _availableInstances.Count > 0
                    ? GetFirstAvailableInstance()
                    : CreateNewInstance();

            return SaveUsedInstance(instance);
        }

        public T ReleaseInstance(T instance)
        {
            instance.gameObject.SetActive(false);
            _usedInstances.Remove(instance);
            _availableInstances.Add(instance);
            return instance;
        }

        private T CreateNewInstance()
        {
            return Object.Instantiate(_prefab);
        }

        private T SaveUsedInstance(T instance)
        {
            _usedInstances.Add(instance);
            return instance;
        }

        private T GetFirstAvailableInstance()
        {
            var instance = _availableInstances[0];
            _availableInstances.RemoveAt(0);
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void DisposeAll()
        {
            _availableInstances.ForEach(instance => Object.Destroy(instance.gameObject));
            _availableInstances.Clear();

            _usedInstances.ForEach(instance => Object.Destroy(instance.gameObject));
            _usedInstances.Clear();
        }

        public IEnumerable<T> GetAllUsed()
        {
            return _usedInstances;
        }
    }
}