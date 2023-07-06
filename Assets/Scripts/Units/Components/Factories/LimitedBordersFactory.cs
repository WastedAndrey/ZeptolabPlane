
using Assets.Scripts.GameSettings;
using UnityEngine;

namespace Assets.Scripts.Units.Components.Factories
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "LimitBordersFactory", menuName = "Game/ComponentFactories/LimitBordersFactory")]
    public class LimitedBordersFactory : ComponentFactoryBase
    {
        [SerializeField]
        private TargetBorders _targetBorders;

        public override ComponentBase Create(Unit plane, UnitContext planeContext)
        {
            return new LimitedBordersComponent(plane, planeContext, _targetBorders);
        }
    }
}