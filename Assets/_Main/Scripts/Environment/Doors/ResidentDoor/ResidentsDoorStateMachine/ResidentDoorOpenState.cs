using _Main.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ResidentDoor.ResidentsDoorStateMachine
{
    public class ResidentDoorOpenState : IState
    {
        private Vector3 _closedAngle;
        private Vector3 _openAngle;
        private Transform _doorRoot;
        private float _rotationSpeed;
        private ResidentDoorStateMachine _doorStateMachine;
        private ResidentDoorKnocker _residentDoorKnocker;
        private ResidentObjectPlace _residentObjectPlace;

        private bool _done;
        private float _factor;
        
        public ResidentDoorOpenState(ResidentDoorStateMachine doorStateMachine, 
            Vector3 closedAngle,
            Vector3 openAngle,
            Transform doorRoot,
            float rotationSpeed,
            ResidentDoorKnocker doorKnocker,
            ResidentObjectPlace residentObjectPlace)
        {
            _doorStateMachine = doorStateMachine;
            _closedAngle = closedAngle;
            _openAngle = openAngle;
            _doorRoot = doorRoot;
            _rotationSpeed = rotationSpeed;
            _residentDoorKnocker = doorKnocker;
            _residentObjectPlace = residentObjectPlace;
        }
        public void Enter()
        {
            _done = false;
            _factor = 0f;
            _residentDoorKnocker.gameObject.SetActive(false);
            _residentObjectPlace.gameObject.SetActive(true);
        }

        public void Update()
        {
            if (_done)
                return;
            
            if (!_done && _factor < 1f)
            {
                _doorRoot.localEulerAngles = Vector3.Lerp(_closedAngle, _openAngle, _factor);
                _factor += Time.deltaTime * _rotationSpeed;
                return;
            }
            _done = true;
            _doorRoot.localEulerAngles = _openAngle;
        }

        public void Dispose()
        {
            _residentDoorKnocker?.gameObject.SetActive(true);
            _residentObjectPlace?.gameObject.SetActive(false);
        }
    }
}