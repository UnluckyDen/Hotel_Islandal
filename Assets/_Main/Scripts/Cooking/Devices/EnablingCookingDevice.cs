using System;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.Cooking.Devices
{
    public class EnablingCookingDevice : CookingDevice
    {
        [SerializeField] private DeviceButton _deviceButton;

        public override bool IsEmpty { get; }

        private void Start()
        {
            _deviceButton.Subscribe(DeviceButtonOnClick);
        }

        private void OnDestroy()
        {
            _deviceButton.Unsubscribe(DeviceButtonOnClick);
        }

        public override bool PlaceMovableObject(IMovableObject movableObject)
        {
            return base.PlaceMovableObject(movableObject);
        }

        public override IMovableObject TakeMovableObject()
        {
            return base.TakeMovableObject();
        }

        private void DeviceButtonOnClick(bool click)
        {
            
        }
    }
}