using _Main.Scripts.Interfaces;
using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.PlayerObjectManipulator
{
    public class PlayerObjectManipulator : MonoBehaviour
    {
        [SerializeField] private float _manipulatorDistance;
        [SerializeField] private LayerMask _manipulatorLayerMask;
        [SerializeField] private PlayerManipulatorPlace _manipulatorPlace;

        private Camera _camera;
        private InputService _inputService;
        
        private IHoverable _hoverableObject;
        private IMovableObject _movableObject;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _inputService = InputService.Instance;
            _inputService.Click += InputServiceOnClick;
        }

        private void OnDestroy()
        {
            _inputService.Click -= InputServiceOnClick;
        }

        private void Update()
        {
            HandleHoverable();
        }

        private void InputServiceOnClick(bool clicked)
        {
            IHoverable hoverable = GetIHoverable();
            
            if (hoverable == null || !clicked) 
                return;
            
            switch (hoverable)
            {
                case IInteractable interactable:
                    interactable.OnClick();
                    break;
                
                case IMovableObject movableObject:
                    if (_manipulatorPlace.PlaceMovableObject(movableObject))
                        Debug.Log("Take");
                    break;
                
                case IObjectPlace objectPlace:
                    if (!_manipulatorPlace.IsEmpty && (objectPlace.IsEmpty || objectPlace.MayContainMultipleObjects))
                    {
                        objectPlace.PlaceMovableObject(_manipulatorPlace.TakeMovableObject());
                        Debug.Log("ToObjectPlace");
                    }
                    else if (_manipulatorPlace.IsEmpty && !objectPlace.IsEmpty)
                    {
                        _manipulatorPlace.PlaceMovableObject(objectPlace.TakeMovableObject());
                        Debug.Log("ToManipulator");
                    }
                    break;
            }
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
    }
}