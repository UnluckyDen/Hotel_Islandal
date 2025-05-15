using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cooking.Devices;
using _Main.Scripts.Environment.Doors;
using UnityEngine;

namespace _Main.Scripts.Environment.Elevators
{
    public class CoinReceiverMachine : MonoBehaviour
    {
        public event Action CoinsCollectedButtonPressed;
        
        [SerializeField] private List<CoinReceiverPlace> _receiverPlaces;
        [SerializeField] private DeviceButton _deviceButton;
        [SerializeField] private DoorSign _doorSign;

        private int _coinsCount;

        public void Init()
        {
            _deviceButton.ButtonPressed += DeviceButtonOnButtonPressed;
        }

        public void Destruct()
        {
            _deviceButton.ButtonPressed -= DeviceButtonOnButtonPressed;
        }

        public void SetLevelSettings(int coinsCount, int levelNumber)
        {
            _coinsCount = coinsCount;
            _doorSign.SetNumber(levelNumber);
            UpdateNeedCoinReceivers();
        }

        public void DestroyCollectedCoins()
        {
            foreach (var receiverPlace in _receiverPlaces)
                receiverPlace.DestroyCollectedCoin();
        }

        private void UpdateNeedCoinReceivers()
        {
            int currentReceiverPlaceNumber = 1;
            foreach (var receiverPlace in _receiverPlaces)
            {
                if (currentReceiverPlaceNumber <= _coinsCount)
                    receiverPlace.OpenAperture();
                else
                    receiverPlace.CloseAperture();

                currentReceiverPlaceNumber++;
            }
        }

        private void DeviceButtonOnButtonPressed(bool pressed)
        {
            bool canEndLevel = _receiverPlaces.All(receiverPlace => !receiverPlace.IsOpen || receiverPlace.CurrentMovableObject != null);

            if (canEndLevel)
                CoinsCollectedButtonPressed?.Invoke();
        }
    }
}