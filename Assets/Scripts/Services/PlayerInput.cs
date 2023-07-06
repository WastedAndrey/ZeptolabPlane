
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class PlayerInput : ServiceBase
    {
        private bool _keyboardInput = false;
        private Vector2 _joystickInput = new Vector2();
        private bool[] _buttonInput = new bool[3];

        public Vector2 JoystickInput { get => _joystickInput; private set => _joystickInput = value; }
        public bool[] ButtonInput { get => _buttonInput; private set => _buttonInput = value; }

        public void SetJoystickInput(Vector2 joystickInput)
        {
            if (_keyboardInput)
                return;

            _joystickInput = joystickInput;
        }

        public void SetButtonInput(int buttonIndex, bool buttonPressed)
        {
            if (_keyboardInput)
                return;

            _buttonInput[buttonIndex] = buttonPressed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _keyboardInput = !_keyboardInput;
                Debug.Log($"Keyboard Input: {_keyboardInput}");
            }
                

            UpdateInputInEditor();
        }

        private void UpdateInputInEditor()
        {
            if (!_keyboardInput)
                return;

            _joystickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            _buttonInput[0] = Input.GetKey(KeyCode.Q);
            _buttonInput[1] = Input.GetKey(KeyCode.E);
        }
    }
}