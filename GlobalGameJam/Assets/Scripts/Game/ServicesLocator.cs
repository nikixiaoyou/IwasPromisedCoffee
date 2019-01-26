using System;
using System.Collections;
using System.Collections.Generic;

namespace ggj
{
    public class ServicesLocator
    {
        private Dictionary<Type, Object> _servicesMap = new Dictionary<Type, Object>();

        public void Register<T>(T instance) where T : new()
        {
            var type = typeof(T);
            if (!_servicesMap.ContainsKey(type))
            {
                _servicesMap.Add(type, instance);
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("Service is already there");
            }
        }

        public void UnRegister<T>(T instance) where T : new()
        {
            var type = typeof(T);
            if (_servicesMap.ContainsKey(type))
            {
                _servicesMap.Remove(type);
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("Service is already there");
            }
        }

        public void UnregisterAll()
        {
            _servicesMap.Clear();
        }

        public T Get<T>() where T : new()
        {
            return (T)_servicesMap[typeof(T)];
        }

        public T TryGet<T>() where T : new()
        {
            _servicesMap.TryGetValue(typeof(T), out object instance);
            return (T)instance;
        }

        public void GetAsync<T>(Action<T> onLoad) where T : new()
        {
            var instance = TryGet<T>();
            if(instance != null)
            {
                onLoad(instance);
            }
            GameManager.Instance.StartCoroutine(GetAsyncEnum<T>(onLoad));
        }

        private IEnumerator GetAsyncEnum<T>(Action<T> onLoad) where T : new()
        {
            var type = typeof(T);
            while(!_servicesMap.ContainsKey(type))
            {
                yield return null;
            }
            onLoad(Get<T>());
        }
    }
}