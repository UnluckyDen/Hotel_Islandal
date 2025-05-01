using _Main.Scripts.Environment.Doors.ResidentDoor.ResidentsDoorStateMachine;
using _Main.Scripts.Interfaces;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ResidentDoor
{
    public class ResidentDoor : MonoBehaviour, IHoverable
    {
        [SerializeField] private BaseHoverGroup _hoverGroup;
        [SerializeField] private DoorSign _doorSign;
        [SerializeField] private Vector3 _closeAngle;
        [SerializeField] private Vector3 _openAngele;
        [SerializeField] private Transform _doorRoot;
        [SerializeField] private float _rotationSpeed = 4;

        private ResidentDoorStateMachine _doorStateMachine;

        private bool _inited;

        public void Init(ResidentDoorKnocker residentDoorKnocker, ResidentObjectPlace residentObjectPlace)
        {
            _doorStateMachine = new ResidentDoorStateMachine(_closeAngle, _openAngele, _doorRoot, _rotationSpeed, residentDoorKnocker, residentObjectPlace);
            _doorStateMachine.ToClose();
            _inited = true;
        }

        public void Destruct() =>
            _doorStateMachine.Dispose();

        private void Update()
        {
            if (!_inited)
                return;
            
            _doorStateMachine.UpdateStates();
        }

        public void OpenDoor()
        {
            _doorStateMachine.ToOpen();
        }
        
        public void CloseDoor()
        {
            _doorStateMachine.ToClose();
        }
        
        public void OnHoverEnter()
        {
            _hoverGroup.OnHoverEnter();
        }

        public void OnHoverExit()
        {
            _hoverGroup.OnHoverExit();
        }
    }
}