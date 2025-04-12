using _Main.Scripts.Interfaces;
using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.PlayerObjectManipulator
{
    public class PlayerObjectManipulator : MonoBehaviour
    {
        [SerializeField] private float _manipulatorDistance;
        [SerializeField] private LayerMask _manipulatorLayerMask;

        private Camera _camera;
        private InputService _inputService;
        private IInteractable _hoverInteractableObject;

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
            HandleRayCast();
        }

        private void InputServiceOnClick(bool clicked)
        {
            if (_hoverInteractableObject != null && clicked)
                _hoverInteractableObject.OnClick();
        }

        private void HandleRayCast()
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

            Debug.DrawRay(ray.origin, ray.direction * _manipulatorDistance, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, _manipulatorDistance, _manipulatorLayerMask))
            {
                IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();

                if (interactableObject == null)
                {
                    ClearCurrentMovable();
                    return;
                }

                if (_hoverInteractableObject == interactableObject) 
                    return;
                
                _hoverInteractableObject?.OnHoverExit();
                _hoverInteractableObject = interactableObject;
                _hoverInteractableObject.OnHoverEnter();

                return;
            }

            ClearCurrentMovable();
        }

        private void ClearCurrentMovable()
        {
            if (_hoverInteractableObject != null)
            {
                _hoverInteractableObject.OnHoverExit();
                _hoverInteractableObject = null;
            }
        }
    }
}