using _Main.Scripts.Environment.Doors.StateMachine;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors
{
    public class ResidentDoor : MonoBehaviour, IObjectPlace, IHoverable
    {
        [SerializeField] private DoorSign _doorSign;
        [SerializeField] private Vector3 _closeAngle;
        [SerializeField] private Vector3 _openAngele;
        [SerializeField] private Transform _doorRoot;
        [SerializeField] private float _rotationSpeed = 4;

        private DoorStateMachine _doorStateMachine;
        
        private void Awake()
        {
            _doorStateMachine = new DoorStateMachine(_closeAngle, _openAngele, _doorRoot, _rotationSpeed);
            _doorStateMachine.ToClose();
        }

        private void OnDestroy() =>
            _doorStateMachine.Dispose();

        private void Update() =>
            _doorStateMachine.UpdateStates();
        
        public bool TryPlaceMovableObject(IMovableObject movableObject)
        {
            _doorStateMachine.OnClick();
            return false;
        }

        public IMovableObject TryTakeMovableObject()
        {
            return null;
        }

        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }
    }
}