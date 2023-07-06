
using UnityEngine;

namespace Assets.Scripts.Units.Components
{
    [System.Serializable]
    public class BossAISettings
    {
        [SerializeField]
        private float _shootingDelay = 0;
        [SerializeField]
        private float _distance = -50;
        [SerializeField]
        private float _screenMovementSpeed = 5;
        [SerializeField]
        private float _movementChangeCooldown = 5;
        [SerializeField]
        private LayerMask _targetLayerMask;
        [SerializeField]
        private float _findTargetCooldown = 2;


        public float Distance => _distance;
        public float ShootingDelay => _shootingDelay;
        public float ScreenMovementSpeed => _screenMovementSpeed;
        public float MovementChangeCooldown => _movementChangeCooldown;
        public LayerMask TargetLayerMask => _targetLayerMask; 
        public float FindTargetCooldown => _findTargetCooldown; 
    }

    public class BossAIComponent : ComponentBase
    {
        private BossAISettings _settings;
        private float _lifetime;
        private Transform _target;
        private float _movementChangeCooldownCurrent = 0;
        private float _findTargetCooldownCurrent = 0;

        public BossAIComponent(Unit unit, UnitContext unitContext, BossAISettings settings) : base(unit, unitContext)
        {
            _settings = settings;
            FindTarget();
        }

        protected override void Subscribe()
        {
            _unitContext.UnitEvents.Update += Update;
            _unitContext.UnitEvents.FixedUpdate += FixedUpdate;
        }

        protected override void Unsubscribe()
        {
            _unitContext.UnitEvents.Update -= Update;
            _unitContext.UnitEvents.FixedUpdate += FixedUpdate;
        }

        private void Update(float deltaTime)
        {
            _lifetime += deltaTime;

            UpdateTarget();
            UpdateWeapons(deltaTime);
        }
        private void FixedUpdate(float deltaTime)
        {
            UpdateVelocity(deltaTime);
        }
        private void UpdateWeapons(float deltaTime)
        {
            if (_lifetime >= _settings.ShootingDelay)
            {
                for (int i = 0; i < _unitContext.Weapons.Count; i++)
                {
                    _unitContext.Weapons[i].ShootAI(_unitContext.Team, _unit.transform.rotation * Vector3.forward);
                }
            }
        }

        private void UpdateVelocity(float deltaTime)
        {
            _movementChangeCooldownCurrent -= deltaTime;

            Vector3 velocity = _unitContext.Rigidbody.velocity;

            if (_movementChangeCooldownCurrent <= 0)
            {
                _movementChangeCooldownCurrent = _settings.MovementChangeCooldown;
                Vector2 newRandomVelocity = Random.insideUnitCircle;
                velocity.x = newRandomVelocity.x * _settings.ScreenMovementSpeed;
                velocity.y = newRandomVelocity.y * _settings.ScreenMovementSpeed;
            }

            if (_target != null)
            {
                Vector3 targetPosition = _unit.transform.position;
                targetPosition.z = _target.transform.position.z + _settings.Distance;
                velocity.z = (targetPosition.z - _unit.transform.position.z) * 4f;
            }
            

            _unitContext.Rigidbody.velocity = velocity;
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

        private void FindTarget()
        {
            _findTargetCooldownCurrent = _settings.FindTargetCooldown;

            var targets = Physics.OverlapSphere(_unit.transform.position, 1000, _settings.TargetLayerMask);
            for (int i = 0; i < targets.Length; i++)
            {
                Rigidbody rb = targets[i].attachedRigidbody;
                if (rb == null)
                    continue;

                IDamagable damagable = rb.GetComponent<IDamagable>();
                if (damagable == null)
                    continue;

                if (damagable.Team != _unit.Team)
                {
                    _target = damagable.GameObject.transform;
                    _findTargetCooldownCurrent = 0;
                    break;
                }
            }
        }
    }
}