using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.Tutorial
{
    public class StartInputTutorialHint : ColliderEnterTutorialHint
    {
        private void Start()
        {
            InputService.Instance.MovementInput += InputServiceOnMovementInput;
        }

        private void OnDestroy()
        {
            InputService.Instance.MovementInput -= InputServiceOnMovementInput;
        }

        private void InputServiceOnMovementInput(Vector2 direction, bool pressed)
        {
            if (pressed && direction.x > 0 || direction.x < 0)
                HideHint();
        }
    }
}