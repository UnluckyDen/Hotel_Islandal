using _Main.Scripts.Player.Movement;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerLook _playerLook;
        
        private WayController _wayController;
        private InputService _inputService;
        
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
    }
}