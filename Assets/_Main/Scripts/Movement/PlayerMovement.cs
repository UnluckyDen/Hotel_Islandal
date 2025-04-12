using System.Collections;
using _Main.Scripts.Movement.Way;
using _Main.Scripts.Services;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _rotateSpeed = 3f;
        
        private InputService _inputService;
        private Coroutine _currentMoveCoroutine;
        private WayController _wayController;

        private void Awake()
        {
            _wayController = FindAnyObjectByType<WayController>();
        }

        private void Start()
        {
            _inputService = InputService.Instance;
            
            _inputService.MovementInput += InputServiceOnMovementInput;
            transform.position = _wayController.CurrentWayPoint.transform.position;
            transform.rotation = _wayController.CurrentWayPoint.transform.rotation;
        }

        private void OnDestroy()
        {
            _inputService.MovementInput -= InputServiceOnMovementInput;
        }

        private void InputServiceOnMovementInput(Vector2 inputDirection)
        {
            if (_currentMoveCoroutine != null)
                return;
            
            _currentMoveCoroutine = StartCoroutine(MovementCoroutine(inputDirection));
        }

        private IEnumerator MovementCoroutine(Vector2 inputDirection)
        {
            WayPoint nextWayPoint = null;
            if (inputDirection.y > 0)
                nextWayPoint = _wayController.GetNextWayPoint(transform.forward.ToVector3Int());
            
            if (inputDirection.y < 0)
                nextWayPoint = _wayController.GetNextWayPoint(-transform.forward.ToVector3Int());

            if (nextWayPoint != null)
            {
                yield return MoveCoroutine(nextWayPoint);
                _currentMoveCoroutine = null;
                yield break;
            }

            if (inputDirection.x > 0)
                yield return RotateCoroutine(transform.eulerAngles + new Vector3(0,+90,0));
            
            if (inputDirection.x < 0)
                yield return RotateCoroutine(transform.eulerAngles + new Vector3(0,-90,0));

            _currentMoveCoroutine = null;
        }
        
        private IEnumerator MoveCoroutine(WayPoint nextWayPoint)
        {
            float factor = 0f;

            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = nextWayPoint.transform.position;

            while (factor < 1f)
            {
                transform.position = Vector3.Lerp(currentPosition, targetPosition, factor);
                factor += Time.deltaTime * _movementSpeed;
                yield return null;
            }
            
            transform.position = targetPosition;
        }

        private IEnumerator RotateCoroutine(Vector3 targetRotation)
        {
            float factor = 0f;

            Vector3 currentPosition = transform.eulerAngles;

            while (factor < 1f)
            {
                transform.eulerAngles = Vector3.Lerp(currentPosition, targetRotation, factor);
                factor += Time.deltaTime * _rotateSpeed;
                yield return null;
            }
            
            transform.eulerAngles = targetRotation;
        }
    }
}