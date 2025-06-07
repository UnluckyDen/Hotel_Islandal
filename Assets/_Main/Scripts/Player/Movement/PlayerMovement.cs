using _Main.Scripts.Player.Movement.Commands;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.Services;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Player.Movement
{
    public class PlayerMovement : MonoBehaviour, ICoroutineRunner, IPausable
    {
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _rotateSpeed = 1f;
        [SerializeField] private float _canUndoMoveFactor = 0.5f;
        [SerializeField] private float _canUndoRotateFactor = 0.2f;
        [SerializeField] private MovementSound _movementSound;

        private InputService _inputService;
        private WayController _wayController;
        private MovementAsyncCommandQuery _movementAsyncCommandQuery;

        private MoveInput _moveInput;
        private bool _isPause;
        
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

        public void Pause()
        {
            _movementAsyncCommandQuery.PauseQueue();
            _isPause = true;
        }

        public void UnPause()
        {
            _movementAsyncCommandQuery.UnPauseQueue();
            _isPause = false;
        }

        private void InputServiceOnMovementInput(Vector2 inputDirection, bool press)
        {
            if (inputDirection != _moveInput.Input && !press)
                return;
            
            _moveInput.UpdateInput(inputDirection, press);
        }

        private void Update()
        {
            if (_isPause)
                return;
            
            if (_movementAsyncCommandQuery.IsRunning)
            {
                HandleUndo();
                return;
            }
            _movementSound.StopPlayMoveSound();
            if (_moveInput.Pressed && _moveInput.Input.y != 0)
            {
                HandleMove();
                return;
            }

            if (_moveInput.Pressed && _moveInput.Input.x != 0)
                HandleRotate();
        }

        private void HandleUndo()
        {
            if (!_moveInput.Pressed && _moveInput.RepeatCount > 1)
            {
                if (_movementAsyncCommandQuery.RunningCommand.MoveInput == _moveInput.Input)
                    _movementAsyncCommandQuery.RunningCommand.Undo();
            }
        }

        private void HandleMove()
        {
            WayPoint currentWayPoint = _wayController.CurrentWayPoint;
            WayPoint nextWayPoint = _wayController.GetNextWayPoint((transform.forward * Mathf.Sign(_moveInput.Input.y)).ToVector3Int());
            if (nextWayPoint != null)
            {
                _movementAsyncCommandQuery.Append(new MoveAsyncCommand(_moveInput.Input, transform, _movementSpeed, _canUndoMoveFactor, currentWayPoint, nextWayPoint, _wayController));
                _movementAsyncCommandQuery.StartQueue();
                _moveInput.IncreaseRepeatCount();
                _movementSound.PlayMoveSound();
            }
        }

        private void HandleRotate()
        {
            Vector3 targetRotation = Vector3.zero;

            if (_moveInput.Input.x > 0)
                targetRotation = transform.eulerAngles + new Vector3(0, +90, 0);

            if (_moveInput.Input.x < 0)
                targetRotation = transform.eulerAngles + new Vector3(0, -90, 0);
            
            _movementAsyncCommandQuery.Append(new RotateAsyncCommand(_moveInput.Input, transform, _rotateSpeed, _canUndoRotateFactor, targetRotation));
            _movementAsyncCommandQuery.StartQueue();
            _moveInput.IncreaseRepeatCount();
            _movementSound.PlayTurnSound();
        }
    }
}