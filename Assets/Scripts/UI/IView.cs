
using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public interface IView
    {
        event Action<IView> Closed;
        void Close();
        void SetParent(Transform parent);
    }
}