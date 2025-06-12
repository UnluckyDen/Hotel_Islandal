using _Main.Scripts.Player.Movement;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.PortableDevices.Coins;
using _Main.Scripts.Services;
using _Main.Scripts.Utils;
using _Main.Scripts.Utils.GlobalEvents.Events;
using UnityEngine;
using EventProvider = _Main.Scripts.Utils.GlobalEvents.EventProvider;

namespace _Main.Scripts.Player
{
    public class Player : MonoBehaviour, IPausable
    {
        [SerializeField] private PlayerMindController _playerMindController;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerLook _playerLook;
        [SerializeField] private PlayerObjectManipulator.PlayerObjectManipulator _playerObjectManipulator;
        [Space]
        [SerializeField] private CoinStash _coinStash;
        
        private WayController _wayController;
        private InputService _inputService;

        public CoinStash CoinStash => _coinStash;
        public Transform CameraRoot => _playerLook.transform;
        
        public void Init(WayController wayController, InputService inputService)
        {
            _wayController = wayController;
            _inputService = inputService;

            _playerMindController.Init(EventProvider.Instance);
            _playerMovement.Init(_wayController, _inputService);
            _playerLook.Init(_inputService);
            
            EventProvider.Instance.Subscribe<BookOpenedEvent>(BookOpen);
        }

        public void Destruct()
        {
            _playerMindController.Destruct();
            _playerMovement.Destruct();
            _playerLook.Destruct();
            
            EventProvider.Instance.UnSubscribe<BookOpenedEvent>(BookOpen);
        }

        public void LockCameraAtObject(bool isLock, Transform target = null)
        {
            _playerLook.LookCameraAtObject(isLock, target);
        }

        public void Pause()
        {
            _playerMovement.Pause();
            _playerLook.Pause();
            _playerObjectManipulator.Pause();
        }

        public void UnPause()
        {
            _playerMovement.UnPause();
            _playerLook.UnPause();
            _playerObjectManipulator.UnPause();
        }

        private void BookOpen(BookOpenedEvent bookOpenedEvent)
        {
            if (bookOpenedEvent.IsOpen)
                Pause();
            else
                UnPause();
        }
    }
}