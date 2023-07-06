
using UnityEngine;

namespace Assets.Scripts.Units.Components
{
    [System.Serializable]
    //[CreateAssetMenu(fileName = "SomeComponent", menuName = "Game/ComponentFactories/SomeComponent")]
    public abstract class ComponentFactoryBase : ScriptableObject
    {
        public abstract ComponentBase Create(Unit plane, UnitContext planeContext);
    }
}