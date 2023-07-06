
using System;
using UnityEngine;

namespace Assets.Scripts.General
{
    [System.Serializable]
    public class ReactiveProperty<T> : IReactivePropertyReadOnly<T>, IDisposable
    {
        [SerializeField]
        private T _value;
        public T Value { get => _value; set { _value = value; Changed?.Invoke(_value); } }
        public event Action<T> Changed;


        public ReactiveProperty() { }

        public ReactiveProperty(T value)
        {
            _value = value;
        }

        public void Dispose()
        {
            Changed = null;
        }
    }
}