
using Assets.Scripts.GameSettings;
using UnityEngine;

namespace Assets.Scripts.Units.Components
{
    [System.Serializable]
    public enum TargetBorders
    { 
        Player,
        Enemy
    }

    public class LimitedBordersComponent : ComponentBase
    {
        private Borders _borders;

        public LimitedBordersComponent(Unit unit, UnitContext unitContext, TargetBorders targetBorders) : base(unit, unitContext)
        {
            switch (targetBorders)
            {
                case TargetBorders.Player:
                    _borders = _unitContext.LevelSettings.PlayerBorders;
                    break;
                case TargetBorders.Enemy:
                    _borders = _unitContext.LevelSettings.EnemyBorders;
                    break;
                default:
                    _borders = _unitContext.LevelSettings.PlayerBorders;
                    break;
            }
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
            if (_unitContext.IsAlive == false)
                return;

            Vector3 finalVelocity = _unitContext.Rigidbody.velocity;

            if (_unit.transform.position.x < _borders.BorderHorizontal.x && _unitContext.Rigidbody.velocity.x < 0)
            {
                finalVelocity.x = 0;
            }
            if (_unit.transform.position.x > _borders.BorderHorizontal.y && _unitContext.Rigidbody.velocity.x > 0)
            {
                finalVelocity.x = 0;
            }

            if (_unit.transform.position.y < _borders.BorderVertical.x && _unitContext.Rigidbody.velocity.y < 0)
            {
                finalVelocity.y = 0;
            }
            if (_unit.transform.position.y > _borders.BorderVertical.y && _unitContext.Rigidbody.velocity.y > 0)
            {
                finalVelocity.y = 0;
            }

            _unitContext.Rigidbody.velocity = finalVelocity;
        }
    }
}