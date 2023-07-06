
using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Units.Components
{
    [System.Serializable]
    public class PlayerInputSettings
    {
        [SerializeField]
        private float _screenMovementSpeed = 5; // movement speed along screen (horizontal + vertical)
        [SerializeField]
        private float _rotationSpeed = 30;

        [SerializeField]
        private float _rotationAngleMax = 45;


        public float ScreenMovementSpeed { get => _screenMovementSpeed; private set => _screenMovementSpeed = value; }
        public float RotationSpeed { get => _rotationSpeed; private set => _rotationSpeed = value; }
        public float RotationAngleMax { get => _rotationAngleMax; private set => _rotationAngleMax = value; }
    }

    public class PlayerInputComponent : ComponentBase
    {
        private PlayerInputSettings _settings;
        private PlayerInput _inputService;

        private Vector2 _rotation = new Vector2();

        public PlayerInputComponent(Unit unit, UnitContext unitContext, PlayerInputSettings settings) : base(unit, unitContext)
        {
            _settings = settings;
            _inputService = ServiceLocator.Instance.GetService<PlayerInput>();
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

            UpdateRotation();
            UpdateVelocity();
            UpdateWeapons();
        }

        private void UpdateRotation()
        {
            Vector2 targetAngle = _inputService.JoystickInput * _settings.RotationAngleMax;
            Quaternion targetRotation = Quaternion.Euler(-targetAngle.y, targetAngle.x, -targetAngle.x * 0.85f);

            _unitContext.Model.transform.rotation = Quaternion.RotateTowards(_unitContext.Model.transform.rotation, targetRotation, _settings.RotationSpeed * Time.deltaTime);
        }

        private void UpdateVelocity()
        { 
            Vector3 finalVelocity = _unit.transform.rotation * Vector3.forward * _unitContext.StatsCurrent.MovementSpeed;
            finalVelocity.x += _inputService.JoystickInput.x * _settings.ScreenMovementSpeed;
            finalVelocity.y += _inputService.JoystickInput.y * _settings.ScreenMovementSpeed;

            if (_unit.transform.position.y < _unitContext.LevelSettings.WaterHeight)
                finalVelocity.z *= _unitContext.LevelSettings.UnderwaterSpeedMultiplier;

            _unitContext.Rigidbody.velocity = finalVelocity;
        }

        private void UpdateWeapons()
        {
            int min = Mathf.Min(_unitContext.Weapons.Count, _inputService.ButtonInput.Length);
            for (int i = 0; i < min; i++)
            {
                if (_inputService.ButtonInput[i])
                    _unitContext.Weapons[i].Shoot(_unitContext.Team, Vector3.forward);
            }
        }

    }
}