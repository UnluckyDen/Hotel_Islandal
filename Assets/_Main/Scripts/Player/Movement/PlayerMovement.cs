using _Main.Scripts.Player.Movement.Commands;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.Services;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Player.Movement
{
    public class PlayerMovement : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _rotateSpeed = 1f;
        [SerializeField] private float _canUndoFactor = 0.2f;
        [SerializeField] private MovementSound _movementSound;

        private InputService _inputService;
        private WayController _wayController;
        private MovementAsyncCommandQuery _movementAsyncCommandQuery;

        private MoveInput _moveInput;
        
        public void Init(WayController wayController, InputService inputService)
        {
            _wayController = wayController;
            _inputService = inputService;
            
            _movementAsyncCommandQuery = new MovementAsyncCommandQuery(this);
            _moveInput = new MoveInput();
            
            _inputService.MovementInput += InputServiceOnMovementInput;
            transform.position = _wayController.CurrentWayPoint.transform.position;
            transform.rotation = _wayController.CurrentWayPoint.transform.rotation;
        }

        public void Destruct()
        {
            _inputService.MovementInput -= InputServiceOnMovementInput;
            _movementAsyncCommandQuery.DiscardAllCommands();
        }

        private void InputServiceOnMovementInput(Vector2 inputDirection, bool press)
        {
            if (inputDirection != _moveInput.Input && !press)
                return;
            
            _moveInput.UpdateInput(inputDirection, press);
        }

        private void Update()
        {
            if (_movementAsyncCommandQuery.IsRunning)
                return;

            if (_moveInput.Pressed && _moveInput.Input.y != 0)
            {
                WayPoint wayPoint = _wayController.GetNextWayPoint((transform.forward * Mathf.Sign(_moveInput.Input.y)).ToVector3Int());
                if (wayPoint != null)
                {
                    _movementAsyncCommandQuery.Append(new MoveAsyncCommand(_moveInput, transform, _movementSpeed, wayPoint));
                    _movementAsyncCommandQuery.StartQueue();
                }
                
                return;
            }

            if (_moveInput.Pressed && _moveInput.Input.x != 0)
            {
                Vector3 targetRotation = Vector3.zero;

                if (_moveInput.Input.x > 0)
                    targetRotation = transform.eulerAngles + new Vector3(0, +90, 0);

                if (_moveInput.Input.x < 0)
                    targetRotation = transform.eulerAngles + new Vector3(0, -90, 0);

                Rotate(targetRotation);
            }
        }

        private void Rotate(Vector3 targetRotation)
        {
            _movementAsyncCommandQuery.Append(new RotateAsyncCommand(_moveInput, transform, _rotateSpeed,
                targetRotation));
            _movementAsyncCommandQuery.StartQueue();
        }
    }
}