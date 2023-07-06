
using UnityEngine;

namespace Assets.Scripts.Units.Components
{
    public class ConstantMovementComponent : ComponentBase
    {
        public ConstantMovementComponent(Unit unit, UnitContext unitContext) : base(unit, unitContext)
        {
            OnEnable();
        }

        protected override void Subscribe()
        {
            _unitContext.UnitEvents.OnEnable += OnEnable;
        }

        protected override void Unsubscribe()
        {
            _unitContext.UnitEvents.OnEnable -= OnEnable;
        }

        private void OnEnable()
        {
            if (_unitContext.IsAlive == false)
                return;
            
            _unitContext.Rigidbody.velocity = _unit.transform.rotation * Vector3.forward * _unitContext.StatsCurrent.MovementSpeed;
        }
    }
}