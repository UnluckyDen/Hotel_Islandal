using _Main.Scripts.Environment.Doors.ResidentDoor;
using UnityEngine;

namespace _Main.Scripts.Tutorial
{
    public class KnockDoorTutorialHint : ColliderEnterTutorialHint
    {
        [SerializeField] private ResidentDoorKnocker _residentDoorKnocker;
        private void Start()
        {
            _residentDoorKnocker.DoorKnocked += ResidentDoorKnockerOnDoorKnocked;
        }

        private void OnDestroy()
        {
            _residentDoorKnocker.DoorKnocked -= ResidentDoorKnockerOnDoorKnocked;
        }

        private void ResidentDoorKnockerOnDoorKnocked()
        {
            HideHint();
        }
    }
}