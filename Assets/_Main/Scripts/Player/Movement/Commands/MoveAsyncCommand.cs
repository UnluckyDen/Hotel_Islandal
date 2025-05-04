using System.Collections;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.Utils.Commands;
using UnityEngine;

namespace _Main.Scripts.Player.Movement.Commands
{
    public class MoveAsyncCommand : IMoveCommand
    {
        public CommandStatus Status { get; private set; }
        public int CommandNumber { get; private set; }
        public Vector2 MoveInput => _moveInput;

        private readonly Vector2 _moveInput;
        private readonly Transform _playerTransform;
        private readonly float _movementSpeed;
        private readonly float _canUndoFactor;
        private readonly WayPoint _currentWayPoint;
        private readonly WayPoint _nextWayPoint;
        private readonly WayController _wayController;

        private StopFlag _stopFlag;
        private bool _undo;
        private float _currentMoveFactor;

        public MoveAsyncCommand(Vector2 moveInput,
            Transform playerTransform,
            float movementSpeed,
            float canUndoFactor,
            WayPoint currentWayPoint,
            WayPoint nextWayPoint,
            WayController wayController)
        {
            _moveInput = moveInput;
            _playerTransform = playerTransform;
            _movementSpeed = movementSpeed;
            _canUndoFactor = canUndoFactor;
            _currentWayPoint = currentWayPoint;
            _nextWayPoint = nextWayPoint;
            _wayController = wayController;
            
            Status = CommandStatus.NotStarted;
        }

        public IEnumerator Execute(StopFlag stopFlag)
        {
            _stopFlag = stopFlag;
            Status = CommandStatus.Running;
            yield return MoveCoroutine();
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
        
        private IEnumerator MoveCoroutine()
        {
            float factor = 0f;

            Vector3 currentPosition = _playerTransform.position;
            Vector3 targetPosition = _nextWayPoint.transform.position;
            
            while (factor < 1f)
            {
                if (_stopFlag.IsStop)
                {
                    Status = CommandStatus.Interrupted;
                    yield break;
                }
                
                if (_undo)
                    yield break;
                
                _playerTransform.position = Vector3.Lerp(currentPosition, targetPosition, factor);
                factor += Time.deltaTime * _movementSpeed;
                _currentMoveFactor = factor;
                yield return null;
            }
            
            _playerTransform.position = targetPosition;
            _wayController.UpdateCurrentWayPoint(_nextWayPoint);
            
            Status = CommandStatus.Success;
        }

        private IEnumerator UndoCoroutine()
        {
            if (!_undo)
                yield break;
            
            float factor = 1f - _currentMoveFactor;

            Vector3 currentPosition = _nextWayPoint.transform.position;
            Vector3 targetPosition = _currentWayPoint.transform.position;
            
            while (factor < 1f)
            {
                if (_stopFlag.IsStop)
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