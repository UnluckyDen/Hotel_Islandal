using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.Player.Movement
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Vector2 _mouseSensitivity = new Vector2(3f, 3f);

        [SerializeField] private Vector2 _minAngle = new Vector2(-80,-80);
        [SerializeField] private Vector2 _maxAngle = new Vector2(80,80);

        private Vector3 _rotation;

        private InputService _inputService;
        
        public void Init(InputService inputService)
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputService = inputService;
            
            _inputService.LookInput += InputServiceOnLookInput;
        }
        
        public void Destruct()
        {
            Cursor.lockState = CursorLockMode.None;
            _inputService.LookInput -= InputServiceOnLookInput;
        }

        private void InputServiceOnLookInput(Vector2 input)
        {
            _rotation.y += input.x * _mouseSensitivity.x;
            _rotation.x -= input.y * _mouseSensitivity.y;

            _rotation.x = Mathf.Clamp(_rotation.x, _minAngle.y, _maxAngle.y);
            _rotation.y = Mathf.Clamp(_rotation.y, _minAngle.x, _maxAngle.x);
            
            transform.localEulerAngles = new Vector3(_rotation.x, _rotation.y);
        }
    }
}