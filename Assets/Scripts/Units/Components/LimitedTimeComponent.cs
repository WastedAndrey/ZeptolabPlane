
namespace Assets.Scripts.Units.Components
{
    public class LimitedTimeComponent : ComponentBase
    {
        private float _time;

        public LimitedTimeComponent(Unit unit, UnitContext unitContext, float time) : base(unit, unitContext)
        {
            _time = time;
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
            if (_unitContext.IsAlive == false)
                return;

            _time -= deltaTime;
            if (_time <= 0)
                _unit.Die();
        }
    }
}