using Assets.Scripts.UI.Bar;
using Assets.Scripts.UI.Elements;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public class GameWindowView : ViewBase<GameWindowViewModel>
    {
        [Header("Dependencies")]
        [SerializeField]
        private BarView _progressBar;
        [SerializeField]
        private Joystick _joystick;
        [SerializeField]
        private ButtonExtended[] _buttons;
        [SerializeField]
        private Transform _healthParent;

        [Header("Prefabs")]
        [SerializeField]
        private GameObject _healthIconPrefab;
        private List<GameObject> _healthIcons = new List<GameObject>();


        protected override void InitInternal()
        {
            _progressBar.Init(_viewModel.GetProgressBarViewModel());
            CreateHealthIcons();
            UpdateHealth(_viewModel.Health.Value);
        }

        protected override void Subscribe()
        {
            _viewModel.Health.Changed += UpdateHealth;
        }

        protected override void Unsubscribe()
        {
            _viewModel.Health.Changed -= UpdateHealth;
        }

        private void Update()
        {
            _viewModel.SetJoystickInput(_joystick.Direction);
            for (int i = 0; i < _buttons.Length; i++)
            {
                _viewModel.SetButtonInput(i, _buttons[i].IsPressed);
            }
        }

        private void UpdateHealth(int health)
        {
            for (int i = 0; i < _healthIcons.Count; i++)
            {
                _healthIcons[i].SetActive(i < _viewModel.Health.Value);
            }
        }


        private void CreateHealthIcons()
        {
            for (int i = 0; i < _viewModel.HealthMax.Value; i++)
            {
                CreateHealthIcon();
            }
        }
        private void CreateHealthIcon()
        {
            GameObject newHealthIcon = Instantiate(_healthIconPrefab, _healthParent);
            _healthIcons.Add(newHealthIcon);
        }
    }
}