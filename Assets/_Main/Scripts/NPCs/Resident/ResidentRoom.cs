using _Main.Scripts.Environment;
using _Main.Scripts.Environment.Doors;
using _Main.Scripts.Environment.Doors.ResidentDoor;
using _Main.Scripts.NPCs.Resident.ResidentRealizations;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class ResidentRoom : MonoBehaviour
    {
        [SerializeField] private ResidentDoor _residentDoor;
        [SerializeField] private ResidentDoorKnocker _residentDoorKnocker;
        [SerializeField] private ResidentObjectPlace _residentObjectPlace;
        [SerializeField] private DoorSign _doorSign;
        [SerializeField] private ResidentCluesController _residentCluesController;

        [SerializeField] private Transform _residentPlace;

        [SerializeField] private PlayerTriggerZone _playerTriggerZone;

        public Transform ResidentPlace => _residentPlace;

        private BaseResident _resident;

        public void Init(BaseResident resident, int roomNumber)
        {
            _resident = resident;
            _doorSign.SetNumber(roomNumber);
            
            _residentCluesController.Init();
            
            _residentDoorKnocker.Init(_residentDoor, _resident);
            _residentObjectPlace.Init(_residentDoor, _resident);

            _residentDoor.Init(_residentDoorKnocker, _residentObjectPlace);
            
            _resident.Init(_residentDoor, _playerTriggerZone, _residentCluesController);
        }

        public void Destruct()
        {
            _residentCluesController.Destruct();
            _residentDoor.Destruct();
            _resident.Destruct();
        }
    }
}