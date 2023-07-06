
using UnityEngine;

namespace Assets.Scripts.Units.Components.Factories
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "BossAIFactory", menuName = "Game/ComponentFactories/BossAIFactory")]
    public class BossAIFactory : ComponentFactoryBase
    {
        [SerializeField]
        private BossAISettings _settings;

        public override ComponentBase Create(Unit plane, UnitContext planeContext)
        {
            return new BossAIComponent(plane, planeContext, _settings);
        }
    }
}