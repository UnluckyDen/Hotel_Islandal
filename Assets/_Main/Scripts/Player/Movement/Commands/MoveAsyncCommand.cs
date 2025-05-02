using System.Collections;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.Utils.Commands;
using UnityEngine;

namespace _Main.Scripts.Player.Movement.Commands
{
    public class MoveAsyncCommand : IAsyncCommand<MoveInput>
    {
        public CommandStatus Status { get; private set; }
        public int CommandNumber { get; private set; }

        private MoveInput _moveInput;
        private Transform _playerTransform;
        private float _movementSpeed;
        private WayPoint _nextWayPoint;

        public MoveAsyncCommand(MoveInput moveInput, Transform playerTransform, float movementSpeed, WayPoint nextWayPoint)
        {
            _moveInput = moveInput;
            _playerTransform = playerTransform;
            _movementSpeed = movementSpeed;
            _nextWayPoint = nextWayPoint;
            
            Status = CommandStatus.NotStarted;
        }

        public IEnumerator Execute(StopFlag stopFlag)
        {
            Status = CommandStatus.Running;
            yield return MoveCoroutine(_nextWayPoint, stopFlag);
        }

        public void UpdateCommandNumber(int commandNumber)
        {
            CommandNumber = commandNumber;
        }

        public void Undo()
        {
        }
        
        private IEnumerator MoveCoroutine(WayPoint nextWayPoint, StopFlag stopFlag)
        {
            float factor = 0f;

            Vector3 currentPosition = _playerTransform.position;
            Vector3 targetPosition = nextWayPoint.transform.position;
            
            while (factor < 1f)
            {
                if (stopFlag.IsStop)
                {
                    Status = CommandStatus.Interrupted;
                    yield break;
                }
                
                _playerTransform.position = Vector3.Lerp(currentPosition, targetPosition, factor);
                factor += Time.deltaTime * _movementSpeed;
                yield return null;
            }
            
            _playerTransform.position = targetPosition;
            Status = CommandStatus.Success;
        }
    }
}