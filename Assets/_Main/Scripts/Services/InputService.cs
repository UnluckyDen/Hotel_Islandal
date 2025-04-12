using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Main.Scripts.Services
{
    public class InputService : MonoBehaviour
    {
        public event Action<Vector2> MovementInput;
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
            var inputVector = new Vector2();
            if (Input.GetKey(KeyCode.W))
                inputVector += new Vector2(0, 1);
            if (Input.GetKey(KeyCode.S))
                inputVector += new Vector2(0, -1);
            if (Input.GetKey(KeyCode.A))
                inputVector += new Vector2(-1, 0);
            if (Input.GetKey(KeyCode.D))
                inputVector += new Vector2(1, 0);

            if (inputVector.magnitude != 0)
                MovementInput?.Invoke(inputVector);
        }
    }
}