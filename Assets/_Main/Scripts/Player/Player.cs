using _Main.Scripts.Player.Movement;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.PortableDevices.Coins;
using _Main.Scripts.Services;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.Player
{
    public class Player : MonoBehaviour, IPausable
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerLook _playerLook;
        [SerializeField] private CoinStash _coinStash;
        
        private WayController _wayController;
        private InputService _inputService;

        public CoinStash CoinStash => _coinStash;
        
        public void Init(WayController wayController, InputService inputService)
        {
            _wayController = wayController;
            _inputService = inputService;
            
            _playerMovement.Init(_wayController, _inputService);
            _playerLook.Init(_inputService);
        }

        public void Destruct()
        {
            _playerMovement.Destruct();
            _playerLook.Destruct();
        }

        public void LockCameraAtObject(bool isLock, Transform target = null)
        {
            _playerLook.LookCameraAtObject(isLock, target);
        }

        public void Pause()
        {
            _playerMovement.Pause();
        }

        public void UnPause()
        {
            _playerMovement.UnPause();
        }
    }
}