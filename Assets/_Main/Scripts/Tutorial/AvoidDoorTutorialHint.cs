using _Main.Scripts.Environment;
using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.Tutorial
{
    public class AvoidDoorTutorialHint : ColliderEnterTutorialHint
    {
        [SerializeField] private PlayerTriggerZone _playerTriggerZone;
        
        private void Start()
        {
            _playerTriggerZone.PlayerEnterTriggerZone += PlayerTriggerZoneOnPlayerEnterTriggerZone;
        }

        private void OnDestroy()
        {
            _playerTriggerZone.PlayerEnterTriggerZone -= PlayerTriggerZoneOnPlayerEnterTriggerZone;
        }

        private void PlayerTriggerZoneOnPlayerEnterTriggerZone(Player.Player player, bool playerIn)
        {
            if (!playerIn)
                HideHint();
        }
    }
}