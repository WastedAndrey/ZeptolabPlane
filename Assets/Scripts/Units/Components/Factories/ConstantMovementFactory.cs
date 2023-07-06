
using UnityEngine;

namespace Assets.Scripts.Units.Components.Factories
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "ConstantMovementFactory", menuName = "Game/ComponentFactories/ConstantMovementFactory")]
    public class ConstantMovementFactory : ComponentFactoryBase
    {
        public override ComponentBase Create(Unit plane, UnitContext planeContext)
        {
            return new ConstantMovementComponent(plane, planeContext);
        }
    }
}