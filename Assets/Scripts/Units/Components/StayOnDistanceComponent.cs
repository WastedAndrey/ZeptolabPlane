
using UnityEngine;

namespace Assets.Scripts.Units.Components
{
    public class StayOnDistanceSettings
    {
        [SerializeField]
        private float _distance;

        public float Distance => _distance;
    }

    public class StayOnDistanceComponent : ComponentBase
    {
        private StayOnDistanceSettings _settings;
        private Transform _target;

        public StayOnDistanceComponent(Unit unit, UnitContext unitContext, StayOnDistanceSettings settings, Transform target) : base(unit, unitContext)
        {
            _settings = settings;
            _target = target;
        }

        protected override void Subscribe()
        {
            _unitContext.UnitEvents.FixedUpdate += FixedUpdate;
        }

        protected override void Unsubscribe()
        {
            _unitContext.UnitEvents.FixedUpdate -= FixedUpdate;
        }

        private void FixedUpdate(float deltaTime)
        {
            Vector3 targetPosition = _unit.transform.position;
        }
    }
}