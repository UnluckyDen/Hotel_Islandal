using _Main.Scripts.Interfaces;
using _Main.Scripts.NPCs.Resident;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ResidentDoor
{
    public class ResidentDoorKnocker : MonoBehaviour, IInteractable
    {
        [SerializeField] private BaseResident _resident;
        private ResidentDoor _residentDoor;

        public void Init(ResidentDoor residentDoor) =>
            _residentDoor = residentDoor;

        public void OnHoverEnter() =>
            _residentDoor.OnHoverEnter();

        public void OnHoverExit() => 
            _residentDoor.OnHoverEnter();

        public void OnClick()
        {
            _resident.HandleKnock();
        }
    }
}