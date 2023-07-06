
using UnityEngine;

namespace Assets.Scripts.Camera
{
    [System.Serializable]
    public abstract class CameraBase : MonoBehaviour
    {
        public abstract void Init(Transform target);
    }
}