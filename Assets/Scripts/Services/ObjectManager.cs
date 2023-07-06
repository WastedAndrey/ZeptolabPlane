
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class ObjectManager : ServiceBase
    {
        public GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            return Instantiate(prefab, position, rotation, parent);
        }

        public T InstantiatePrefab<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour
        {
            return Instantiate(prefab, position, rotation, parent);
        }
    }
}