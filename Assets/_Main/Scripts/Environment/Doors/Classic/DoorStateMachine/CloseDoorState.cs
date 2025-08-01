using UnityEngine;

namespace _Main.Scripts.Environment.Doors.Classic.DoorStateMachine
{
    public class CloseDoorState : IDoorState
    {
        private Vector3 _closedAngle;
        private Vector3 _openAngle;
        private Transform _doorRoot;
        private float _rotationSpeed;
        private DoorStateMachine _doorStateMachine;

        private bool _done;
        private float _factor;
        
        public CloseDoorState(DoorStateMachine doorStateMachine, Vector3 closedAngle, Vector3 openAngle, Transform doorRoot, float rotationSpeed)
        {
            _doorStateMachine = doorStateMachine;
            _closedAngle = closedAngle;
            _openAngle = openAngle;
            _doorRoot = doorRoot;
            _rotationSpeed = rotationSpeed;
        }

        public void OnClick()
        {
            _doorStateMachine.ToOpen();
        }

        public void Enter()
        {
            _done = false;
            _factor = 0f;
        }

        public void Update()
        {
            if (_done)
                return;
            
            if (!_done && _factor < 1f)
            {
                _doorRoot.localEulerAngles = Vector3.Lerp(_openAngle, _closedAngle, _factor);
                _factor += Time.deltaTime * _rotationSpeed;
                return;
            }

            _done = true;
            _doorRoot.localEulerAngles = _closedAngle;
        }

        public void Dispose()
        {
        }
    }
}