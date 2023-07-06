
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Weapons.Missiles
{
    public class Rocket : MissileBase
    {
        [SerializeField]
        private float _findTargetCooldown = 2;
        private float _findTargetCooldownCurrent = 0;

        [SerializeField]
        private float _maxRotationSpeed = 10f;
        [SerializeField]
        private float _angleDeltaMax;
        private Quaternion _startRotation; 

        private Transform _target;

        protected override void InitInternal()
        {
            _startRotation = this.transform.rotation;
            FindTarget();
        }

        protected override void UpdateInternal()
        {
            UpdateTarget();
            UpdateRotation();
            UpdateMovement();
        }

        private void UpdateTarget()
        {
            if (_target == null)
            {
                _findTargetCooldownCurrent -= Time.deltaTime;
                if (_findTargetCooldownCurrent <= 0)
                    FindTarget();
            }
        }

        private void UpdateRotation()
        {
            if (_target == null)
                return;

            Vector3 directionToTarget = (_target.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);


            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _maxRotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, _startRotation) > _angleDeltaMax)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _startRotation, _maxRotationSpeed * 10 * Time.deltaTime);
            }

            _direction = transform.rotation * Vector3.forward;
        }

        private void FindTarget()
        {
            _findTargetCooldownCurrent = _findTargetCooldown;

            var targets = Physics.OverlapSphere(this.transform.position, 1000, _settings.LayerMask);
            for (int i = 0; i < targets.Length; i++)
            {
                Rigidbody rb = targets[i].attachedRigidbody;
                if (rb == null)
                    continue;

                IDamagable damagable = rb.GetComponent<IDamagable>();
                if (damagable == null)
                    continue;

                if (damagable.Team != Team)
                {
                    _target = damagable.GameObject.transform;
                    _findTargetCooldownCurrent = 0;
                    break;
                }
            }
        }
    }
}