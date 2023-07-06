
using UnityEngine;

namespace Assets.Scripts.Units.Components.Factories
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "PlayerInputFactory", menuName = "Game/ComponentFactories/PlayerInputFactory")]
    public class PlayerInputFactory : ComponentFactoryBase
    {
        [SerializeField]
        private PlayerInputSettings _settings;

        public override ComponentBase Create(Unit plane, UnitContext planeContext)
        {
            return new PlayerInputComponent(plane, planeContext, _settings);
        }
    }
}