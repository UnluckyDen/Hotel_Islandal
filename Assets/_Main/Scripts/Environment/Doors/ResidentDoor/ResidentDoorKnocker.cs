using System;
using _Main.Scripts.Interfaces;
using _Main.Scripts.NPCs.Resident.ResidentRealizations;
using UnityEngine;

namespace _Main.Scripts.Environment.Doors.ResidentDoor
{
    public class ResidentDoorKnocker : MonoBehaviour, IInteractable
    {
        public event Action DoorKnocked;
        
        private ResidentDoor _residentDoor;
        private BaseResident _resident;

        public void Init(ResidentDoor residentDoor, BaseResident baseResident)
        {
            _residentDoor = residentDoor;
            _resident = baseResident;
        }

        public void OnHoverEnter() =>
            _residentDoor.OnHoverEnter();

        public void OnHoverExit() => 
            _residentDoor.OnHoverExit();

        public void OnClick()
        {
            _resident.HandleKnock();
            DoorKnocked?.Invoke();
        }
    }
}