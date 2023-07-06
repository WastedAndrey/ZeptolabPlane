
using UnityEngine;

namespace Assets.Scripts.Units.Components
{
    public abstract class ComponentBase
    {
        [SerializeField]
        protected Unit _unit;
        [SerializeField]
        protected UnitContext _unitContext;

        public ComponentBase(Unit unit, UnitContext unitContext)
        {
            _unit = unit;
            _unitContext = unitContext;
            Subscribe();
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();

        public void Destroy()
        {
            Unsubscribe();
        }
    }
}