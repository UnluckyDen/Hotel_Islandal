using _Main.Scripts.Environment;
using _Main.Scripts.Environment.Doors;
using _Main.Scripts.Environment.Doors.ResidentDoor;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class ResidentRoom : MonoBehaviour
    {
        [SerializeField] private ResidentDoor _residentDoor;
        [SerializeField] private ResidentDoorKnocker _residentDoorKnocker;
        [SerializeField] private ResidentObjectPlace _residentObjectPlace;
        [SerializeField] private DoorSign _doorSign;

        [SerializeField] private Transform _residentPlace;
        [SerializeField] private Transform _hintPlace;
        [SerializeField] private Transform _cluesPlace;

        [SerializeField] private PlayerTriggerZone _playerTriggerZone;

        public Transform ResidentPlace => _residentPlace;

        private BaseResident _resident;

        public void Init(BaseResident resident)
        {
            _resident = resident;

            _residentDoorKnocker.Init(_residentDoor, _resident);
            _residentObjectPlace.Init(_residentDoor, _resident);

            _residentDoor.Init(_residentDoorKnocker, _residentObjectPlace);
            
            _resident.Init(_residentDoor, _playerTriggerZone, _hintPlace, _cluesPlace);
        }

        public void Destruct()
        {
            _residentDoor.Destruct();
            _resident.Destruct();
        }
    }
}