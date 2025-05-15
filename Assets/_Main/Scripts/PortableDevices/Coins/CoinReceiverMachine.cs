using System;
using System.Collections.Generic;
using _Main.Scripts.Cooking.Devices;
using UnityEngine;

namespace _Main.Scripts.PortableDevices.Coins
{
    public class CoinReceiverMachine : MonoBehaviour
    {
        [SerializeField] private List<CoinReceiverPlace> _receiverPlaces;
        [SerializeField] private DeviceButton _deviceButton;

        private int _coinsCount;

        public void Init()
        {
            _deviceButton.ButtonPressed += DeviceButtonOnButtonPressed;
        }

        public void Destruct()
        {
            _deviceButton.ButtonPressed -= DeviceButtonOnButtonPressed;
        }

        public void SetNeedCoins(int coinsCount)
        {
            _coinsCount = coinsCount;
            UpdateNeedCoinReceivers();
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
            if (pressed)
                Debug.Log("ElevatorButtonPressed");
        }
    }
}