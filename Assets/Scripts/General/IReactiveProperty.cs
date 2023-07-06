
using System;

namespace Assets.Scripts.General
{
    public interface IReactivePropertyReadOnly<T>
    {
        public T Value { get; }
        public event Action<T> Changed;
    }
}