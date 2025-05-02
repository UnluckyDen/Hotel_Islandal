using System;
using UnityEngine;

namespace _Main.Scripts.Player.Movement
{
    [Serializable]
    public class MoveInput
    {
        public event Action<MoveInput> MoveInputUpdated;
        public Vector2 Input { get; private set; }
        public bool Pressed { get; private set; }

        public void UpdateInput(Vector2 input, bool pressed)
        {
            Input = input;
            Pressed = pressed;
            
            MoveInputUpdated?.Invoke(this);
        }
    }
}