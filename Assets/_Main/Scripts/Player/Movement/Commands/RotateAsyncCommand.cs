using System.Collections;
using _Main.Scripts.Utils.Commands;
using UnityEngine;

namespace _Main.Scripts.Player.Movement.Commands
{
    public class RotateAsyncCommand : IMoveCommand
    {
        public CommandStatus Status { get; private set; }
        public int CommandNumber { get; private set; }
        public Vector2 MoveInput => _moveInput;

        private readonly Vector2 _moveInput;
        private readonly Transform _playerTransform;
        private readonly float _rotateSpeed;
        private readonly float _canUndoFactor;
        private readonly Vector3 _targetRotation;

        private StopFlag _stopFlag;
        private bool _undo;
        private float _currentMoveFactor;

        private Vector3 _currentRotation;

        public RotateAsyncCommand(Vector2 moveInput, Transform playerTransform, float rotateSpeed, float canUndoFactor, Vector3 targetRotation)
        {
            _moveInput = moveInput;
            _playerTransform = playerTransform;
            _rotateSpeed = rotateSpeed;
            _canUndoFactor = canUndoFactor;
            _targetRotation = targetRotation;
            
            Status = CommandStatus.NotStarted;
        }

        public IEnumerator Execute(StopFlag stopFlag)
        {
            _stopFlag = stopFlag;
            Status = CommandStatus.Running;
            yield return RotateCoroutine();
            yield return UndoCoroutine();
        }

        public void UpdateCommandNumber(int commandNumber)
        {
            CommandNumber = commandNumber;
        }

        public void Undo()
        {
            if (_undo)
                return;
            
            _undo = _currentMoveFactor < _canUndoFactor;
        }

        
        private IEnumerator RotateCoroutine()
        {
            float factor = 0f;

            Vector3 currentRotation = _playerTransform.eulerAngles;
            _currentRotation = currentRotation;

            while (factor < 1f)
            {
                if (_stopFlag.IsStop)
                {
                    Status = CommandStatus.Interrupted;
                    yield break;
                }
                
                if (_undo)
                    yield break;
                
                _playerTransform.eulerAngles = Vector3.Lerp(currentRotation, _targetRotation, factor);
                factor += Time.deltaTime * _rotateSpeed;
                _currentMoveFactor = factor;
                yield return null;
            }
            
            _playerTransform.eulerAngles = _targetRotation;
            Status = CommandStatus.Success;
        }
        
        private IEnumerator UndoCoroutine()
        {
            if (!_undo)
                yield break;
            
            float factor = 1f - _currentMoveFactor;
            
            while (factor < 1f)
            {
                if (_stopFlag.IsStop)
                {
                    Status = CommandStatus.Interrupted;
                    yield break;
                }
                
                _playerTransform.eulerAngles = Vector3.Lerp(_targetRotation, _currentRotation, factor);
                factor += Time.deltaTime * _rotateSpeed;
                yield return null;
            }
            
            _playerTransform.eulerAngles = _currentRotation;
            Status = CommandStatus.Success;
        }
    }
}