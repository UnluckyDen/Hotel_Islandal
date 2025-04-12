using System;
using UnityEngine;

namespace _Main.Scripts.Services
{
    public class InputService : MonoBehaviour
    {
        public event Action<Vector2> MovementInput;
        public event Action<Vector2> LookInput;
        public static InputService Instance { get; private set; }

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
    }
}