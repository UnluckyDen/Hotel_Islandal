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

        private Vector2 _moveInput;
        private Transform _playerTransform;
        private float _rotateSpeed;
        private Vector3 _targetRotation;

        public RotateAsyncCommand(Vector2 moveInput, Transform playerTransform, float rotateSpeed, Vector3 targetRotation)
        {
            _moveInput = moveInput;
            _playerTransform = playerTransform;
            _rotateSpeed = rotateSpeed;
            _targetRotation = targetRotation;
            
            Status = CommandStatus.NotStarted;
        }

        public IEnumerator Execute(StopFlag stopFlag)
        {
            Status = CommandStatus.Running;
            yield return RotateCoroutine(_targetRotation, stopFlag);
        }

        public void UpdateCommandNumber(int commandNumber)
        {
            CommandNumber = commandNumber;
        }

        public void Undo()
        {
        }

        
        private IEnumerator RotateCoroutine(Vector3 targetRotation, StopFlag stopFlag)
        {
            float factor = 0f;

            Vector3 currentPosition = _playerTransform.eulerAngles;

            while (factor < 1f)
            {
                if (stopFlag.IsStop)
                {
                    Status = CommandStatus.Interrupted;
                    yield break;
                }
                
                _playerTransform.eulerAngles = Vector3.Lerp(currentPosition, targetRotation, factor);
                factor += Time.deltaTime * _rotateSpeed;
                yield return null;
            }
            
            _playerTransform.eulerAngles = targetRotation;
            Status = CommandStatus.Success;
        }
    }
}