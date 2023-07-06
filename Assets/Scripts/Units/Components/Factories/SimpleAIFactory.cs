
using UnityEngine;

namespace Assets.Scripts.Units.Components.Factories
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "SimpleAIFactory", menuName = "Game/ComponentFactories/SimpleAIFactory")]
    public class SimpleAIFactory : ComponentFactoryBase
    {
        [SerializeField]
        private float _shootingDelay;

        public override ComponentBase Create(Unit plane, UnitContext planeContext)
        {
            return new SimpleAIComponent(plane, planeContext, _shootingDelay);
        }
    }
}