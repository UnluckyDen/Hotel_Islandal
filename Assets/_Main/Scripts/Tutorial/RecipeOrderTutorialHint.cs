using _Main.Scripts.Environment.Doors.ResidentDoor;
using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.Tutorial
{
    public class RecipeOrderTutorialHint : BaseTutorialHint
    {
        [SerializeField] private ResidentDoorKnocker _residentDoorKnocker;
        private void Start()
        {
            _residentDoorKnocker.DoorKnocked += ResidentDoorKnockerOnDoorKnocked;
            InputService.Instance.OpenBook += InputServiceOnOpenBook;
        }

        private void OnDestroy()
        {
            _residentDoorKnocker.DoorKnocked -= ResidentDoorKnockerOnDoorKnocked;
            InputService.Instance.OpenBook -= InputServiceOnOpenBook;
        }

        private void ResidentDoorKnockerOnDoorKnocked()
        {
            ShowHint();
        }

        private void InputServiceOnOpenBook()
        {
            HideHint();
        }
    }
}