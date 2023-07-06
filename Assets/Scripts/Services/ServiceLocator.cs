
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class ServiceLocator : MonoBehaviour
    {
        [SerializeField]
        private List<ServiceBase> _services = new List<ServiceBase>();

        private Dictionary<Type, ServiceBase> _servicesDictionary = new Dictionary<Type, ServiceBase>();

        private static ServiceLocator _instance;
        public static ServiceLocator Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
                Init();
            else
                Destroy(this.gameObject);
        }

        private void Init()
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            for (int i = 0; i < _services.Count; i++)
            {
                RegisterService(_services[i]);
                _services[i].Init();
            }

            GetService<GameManager>().StartGame();
        }
        

        public void RegisterService(ServiceBase serviceInstance)
        {
            Type serviceType = serviceInstance.GetType();
            if (_servicesDictionary.ContainsKey(serviceType) == false)
            {
                _servicesDictionary.Add(serviceType, serviceInstance);
            }
            else
            {
                Debug.LogWarning($"Service {serviceInstance.name} already registered!");
            }
        }

        public T GetService<T>() where T : MonoBehaviour
        {
            if (_servicesDictionary.ContainsKey(typeof(T)))
            {
                return _servicesDictionary[typeof(T)] as T;
            }

            Debug.LogWarning("Service of type " + typeof(T) + " not found!");
            return null;
        }
    }
}