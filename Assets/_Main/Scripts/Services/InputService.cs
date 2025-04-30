using System;
using UnityEngine;

namespace _Main.Scripts.Services
{
    public class InputService : MonoBehaviour
    {
        public event Action<Vector2> MovementInput;
        public event Action<Vector2> LookInput;
        public event Action<bool> Click;
        public event Action<bool> RightClick;
        
        public static InputService Instance { get; private set; }
        public bool MouseButtonClicked { get; private set; }
        public bool MouseButtonRightClicked { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        private void Update()
        {
            ReedMoveInputs();
            ReedLookInputs();
            ReedClick();
            ReedRightClick();
        }

        private void ReedMoveInputs()
        {
            var inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (inputVector.magnitude != 0)
                MovementInput?.Invoke(inputVector);
        }

        private void ReedLookInputs()
        {
            var inputVector = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            
            if (inputVector.magnitude != 0)
                LookInput?.Invoke(inputVector);
        }

        private void ReedClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseButtonClicked = true;
                Click?.Invoke(MouseButtonClicked);
            }

            if (Input.GetMouseButtonDown(0))
            {
                MouseButtonClicked = false;
                Click?.Invoke(MouseButtonClicked);
            }
        }
        
        private void ReedRightClick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                MouseButtonRightClicked = true;
                RightClick?.Invoke(MouseButtonRightClicked);
            }

            if (Input.GetMouseButtonDown(1))
            {
                MouseButtonRightClicked = false;
                RightClick?.Invoke(MouseButtonRightClicked);
            }
        }
    }
}