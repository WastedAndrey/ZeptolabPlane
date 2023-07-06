
using Assets.Scripts.GameLevel;
using Assets.Scripts.General;
using Assets.Scripts.Services;
using Assets.Scripts.UI.Bar;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameWindowViewModel : ViewModelBase
    {
        private Unit _unit;
        private GameLevelSpawner _spawner;
        private PlayerInput InputService => ServiceLocator.Instance.GetService<PlayerInput>();

        private ReactiveProperty<int> _healthMax = new ReactiveProperty<int>();
        private ReactiveProperty<int> _health = new ReactiveProperty<int>();
        public IReactivePropertyReadOnly<int> HealthMax => _healthMax;
        public IReactivePropertyReadOnly<int> Health => _health;


        public GameWindowViewModel(Unit unit, GameLevelSpawner spawner)
        {
            _unit = unit;
            _spawner = spawner;

            _healthMax.Value = _unit.HealthMax;
            _health.Value = _unit.Health;
        }

        public void SetJoystickInput(Vector2 joystickInput)
        {
            InputService.SetJoystickInput(joystickInput);
        }

        public void SetButtonInput(int buttonIndex, bool buttonPressed)
        {
            InputService.SetButtonInput(buttonIndex, buttonPressed);
        }

        public BarViewModelBase GetProgressBarViewModel()
        {
            return new BarViewModelLevelProgress(_spawner);
        }

        public override void OnEnable()
        {
            _unit.DamageReceived += OnUnitDamageReceived;
        }
        public override void OnDisable()
        {
            _unit.DamageReceived -= OnUnitDamageReceived;
        }


        private void OnUnitDamageReceived(Unit unit, int damage)
        {
            _health.Value = _unit.Health;
        }
    }
}