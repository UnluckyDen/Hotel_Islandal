using System;
using _Main.Scripts.Environment.Doors.ElevatorDoor;
using UnityEngine;

namespace _Main.Scripts.Environment.Elevators
{
    public class Elevator : MonoBehaviour
    {
        public event Action LevelConditionsComplete;
        
        [SerializeField] private CoinReceiverMachine _coinReceiverMachine;
        [SerializeField] private ElevatorDoor _elevatorDoor;
        [SerializeField] private PlayerTriggerZone _playerTriggerZone;

        public void Init()
        {
            _coinReceiverMachine.Init();
            _coinReceiverMachine.CoinsCollectedButtonPressed += CoinReceiverMachineOnCoinsCollectedButtonPressed;
        }

        public void Destruct()
        {
            _coinReceiverMachine.CoinsCollectedButtonPressed -= CoinReceiverMachineOnCoinsCollectedButtonPressed;
            _coinReceiverMachine.Destruct();
        }

        public void StartLevel(int coinToLeave, int levelNumber)
        {
            _coinReceiverMachine.SetLevelSettings(coinToLeave, levelNumber);
            _elevatorDoor.OpenDoor();
        }

        private void CoinReceiverMachineOnCoinsCollectedButtonPressed()
        {
            if (_playerTriggerZone.PlayerInZone)
            {
                _coinReceiverMachine.DestroyCollectedCoins();
                _elevatorDoor.CloseDoor();
                _elevatorDoor.DoorStateChangeComplete += opened =>
                {
                    if (!opened)
                        LevelConditionsComplete?.Invoke();
                };
            }
        }
    }
}