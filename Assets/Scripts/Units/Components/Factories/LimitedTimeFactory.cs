
using UnityEngine;

namespace Assets.Scripts.Units.Components.Factories
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "LimitedTimeFactory", menuName = "Game/ComponentFactories/LimitedTimeFactory")]
    public class LimitedTimeFactory : ComponentFactoryBase
    {
        [SerializeField]
        private float _lifetime;

        public override ComponentBase Create(Unit plane, UnitContext planeContext)
        {
            return new LimitedTimeComponent(plane, planeContext, _lifetime);
        }
    }
}