using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices
{
    public class DeviceButton : MonoBehaviour, IInteractable, IPublisher<bool>
    {
        [SerializeField] private Vector3 _defaultPosition;
        [SerializeField] private Vector3 _pressedPosition;
        [SerializeField] private float _pressTime = 0.2f;
        [SerializeField] private ButtonStateType _defaultPressState;

        private readonly List<Action<bool>> _subscribedActions = new();
        private Coroutine _currentCoroutine;
        private ButtonStateType _currentButtonState;

        private void Start()
        {
            if (_defaultPressState == ButtonStateType.Pressed)
                ToPressedState();
            
            if (_defaultPressState == ButtonStateType.NotPressed)
                ToNotPressedState();
        }

        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }

        public void OnClick()
        {
            ChangeState();
        }

        public void Subscribe(Action<bool> action) =>
            _subscribedActions.Add(action);
        public void Unsubscribe(Action<bool> action) =>
            _subscribedActions.Remove(action);

        private void ChangeState()
        {
            if (_currentButtonState == ButtonStateType.Pressed)
                ToNotPressedState();

            if (_currentButtonState == ButtonStateType.NotPressed)
                ToPressedState();
        }

        private void ToNotPressedState() =>
            _currentCoroutine ??= StartCoroutine(PlayUpAnimation());
        

        private void ToPressedState() =>
            _currentCoroutine ??= StartCoroutine(PlayPressAnimation());
        
        private IEnumerator PlayPressAnimation()
        {
            float factor = 0f;

            TriggerActions(true);

            while (factor < 1f)
            {
                factor += Time.deltaTime / _pressTime;
                transform.localPosition = Vector3.Lerp(_defaultPosition, _pressedPosition, factor);
                yield return null;
            }
            
            transform.localPosition = _pressedPosition;
            _currentButtonState = ButtonStateType.Pressed;

            _currentCoroutine = null;
        }
        
        private IEnumerator PlayUpAnimation()
        {
            float factor = 0f;

            TriggerActions(false);
            
            while (factor < 1f)
            {
                factor += Time.deltaTime / _pressTime;
                transform.localPosition = Vector3.Lerp(_pressedPosition, _defaultPosition, factor);
                yield return null;
            }

            transform.localPosition = _defaultPosition;
            _currentButtonState = ButtonStateType.NotPressed;

            _currentCoroutine = null;
        }

        private void TriggerActions(bool press)
        {
            foreach (var action in _subscribedActions)
                action?.Invoke(press);
        }
    }

    public enum ButtonStateType
    {
        NotPressed = 1,
        Pressed = 2
    }
}