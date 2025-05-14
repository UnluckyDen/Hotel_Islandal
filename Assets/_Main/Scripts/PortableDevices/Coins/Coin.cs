using _Main.Scripts.Cooking.Devices;
using _Main.Scripts.Interfaces;
using UnityEngine;

namespace _Main.Scripts.PortableDevices.Coins
{
    public class Coin : Device, IMovableObject, IActivatingObject
    {
        [SerializeField] private Collider _collider;
        
        private bool _rollActive;
        
        public bool IsTrashable => false;
        public bool CurrentActivity => _rollActive;

        public void SwitchActive()
        {
            if (!_rollActive)
                Activate();
            else
                Deactivate();
        }

        public void Activate()
        {
            _rollActive = true;
        }

        public void Deactivate()
        {
            _rollActive = false;
        }

        public void ToNonInteractive()
        {
            _collider.enabled = false;
        }

        public void ToInteractable()
        {
            Deactivate();
            _collider.enabled = true;
        }
    }
}