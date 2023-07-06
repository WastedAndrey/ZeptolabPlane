
using UnityEngine;

namespace Assets.Scripts.Units.Components
{
    public class SimpleAIComponent : ComponentBase
    {
        private float _lifetime = 0;
        private float _shootingDelay = 0;

        public SimpleAIComponent(Unit unit, UnitContext unitContext, float shootingDelay) : base(unit, unitContext)
        {
            _shootingDelay = shootingDelay;
        }

        protected override void Subscribe()
        {
            _unitContext.UnitEvents.Update += Update;
        }

        protected override void Unsubscribe()
        {
            _unitContext.UnitEvents.Update -= Update;
        }

        private void Update(float deltaTime)
        {
            _lifetime += deltaTime;

            if (_lifetime >= _shootingDelay)
            {
                for (int i = 0; i < _unitContext.Weapons.Count; i++)
                {
                    _unitContext.Weapons[i].ShootAI(_unitContext.Team, _unit.transform.rotation * Vector3.forward);
                }
            }
        }
    }
}