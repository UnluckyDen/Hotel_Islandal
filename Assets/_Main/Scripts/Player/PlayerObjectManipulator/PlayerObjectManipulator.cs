using _Main.Scripts.Interfaces;
using _Main.Scripts.Services;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Player.PlayerObjectManipulator
{
    public class PlayerObjectManipulator : MonoBehaviour, IPausable
    {
        [SerializeField] private float _manipulatorDistance;
        [SerializeField] private LayerMask _manipulatorLayerMask;
        [SerializeField] private PlayerManipulatorPlace _manipulatorPlace;

        private Camera _camera;
        private InputService _inputService;
        
        private IHoverable _hoverableObject;
        private IMovableObject _movableObject;

        private bool _pause;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _inputService = InputService.Instance;
            _inputService.Click += InputServiceOnClick;
            _inputService.RightClick += InputServiceOnRightClick;
        }

        private void OnDestroy()
        {
            _inputService.Click -= InputServiceOnClick;
            _inputService.RightClick -= InputServiceOnRightClick;
        }

        private void Update()
        {
            HandleHoverable();
        }

        private void InputServiceOnClick(bool clicked)
        {
            if (_pause)
                return;
            
            IHoverable hoverable = GetIHoverable();
            
            if (hoverable == null || !clicked) 
                return;
            
            switch (hoverable)
            {
                case IInteractable interactable:
                    interactable.OnClick();
                    break;
                
                case IMovableObject movableObject:
                    if (_manipulatorPlace.CanApply(movableObject))
                        _manipulatorPlace.PlaceMovableObject(movableObject);
                    break;
                
                case IObjectPlace objectPlace:
                    if (_manipulatorPlace.CurrentMovableObject != null && objectPlace.CanApply(_manipulatorPlace.CurrentMovableObject))
                        objectPlace.PlaceMovableObject(_manipulatorPlace.TakeMovableObject());
                    else if (_manipulatorPlace.CanApply(objectPlace.CurrentMovableObject))
                        _manipulatorPlace.PlaceMovableObject(objectPlace.TakeMovableObject());
                    
                    break;
            }
        }

        private void InputServiceOnRightClick(bool rightClick)
        {
            if (_pause)
                return;
            
            if (rightClick && _manipulatorPlace.CurrentMovableObject is IActivatingObject activatingObject)
                activatingObject.SwitchActive();
        }

        private void HandleHoverable()
        {
            var hoverable = GetIHoverable();
            
            if (hoverable != null)
            {
                if (_hoverableObject == hoverable) 
                    return;
                
                _hoverableObject?.OnHoverExit();
                _hoverableObject = hoverable;
                _hoverableObject.OnHoverEnter();
                
                return;
            }
            
            ClearCurrentInteractable();
        }

        private IHoverable GetIHoverable()
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

            Debug.DrawRay(ray.origin, ray.direction * _manipulatorDistance, Color.red);

            return Physics.Raycast(ray, out RaycastHit hit, _manipulatorDistance, _manipulatorLayerMask) 
                ? hit.collider.GetComponent<IHoverable>() 
                : null;
        }

        private void ClearCurrentInteractable()
        {
            if (_hoverableObject == null)
                return;
            
            _hoverableObject.OnHoverExit();
            _hoverableObject = null;
        }

        public void Pause()
        {
            _pause = true;
        }

        public void UnPause()
        {
            _pause = false;
        }
    }
}