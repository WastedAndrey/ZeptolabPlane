
using UnityEngine;

namespace Assets.Scripts.Units.Components.Factories
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "UnitsCollisionFactory", menuName = "Game/ComponentFactories/UnitsCollisionFactory")]
    public class UnitsCollisionFactory : ComponentFactoryBase
    {
        public override ComponentBase Create(Unit plane, UnitContext planeContext)
        {
            return new UnitsCollisionComponent(plane, planeContext);
        }
    }
}