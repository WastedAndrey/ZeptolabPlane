
using UnityEngine;

namespace Assets.Scripts.Units.Components
{
    public class UnitsCollisionComponent : ComponentBase
    {
        public UnitsCollisionComponent(Unit unit, UnitContext unitContext) : base(unit, unitContext)
        {
        }

        protected override void Subscribe()
        {
            _unitContext.UnitEvents.OnTriggerEnter += OnTriggerEnter;
        }

        protected override void Unsubscribe()
        {
            _unitContext.UnitEvents.OnTriggerEnter -= OnTriggerEnter;
        }

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody targetRb = other.attachedRigidbody;
            if (targetRb == null)
                return;

            IDamagable target = targetRb.GetComponent<IDamagable>();
            if (target != null && target.Team != _unit.Team)
            {
                target.ReceiveDamage(_unitContext.StatsDefault.Health);
            }
        }
    }
}