using _Main.Scripts.Environment;
using _Main.Scripts.NPCs.Resident.ResidentRealizations;
using UnityEngine;

namespace _Main.Scripts.Tutorial
{
    public class GiveOrderHint : BaseTutorialHint
    {
        [SerializeField] private PlayerTriggerZone _playerTriggerZone;

        private void Start()
        {
            BaseResident.FoodAccepted += BaseResidentOnFoodAccepted;
            _playerTriggerZone.PlayerEnterTriggerZone += PlayerTriggerZoneOnPlayerEnterTriggerZone;
        }

        private void OnDestroy()
        {
            BaseResident.FoodAccepted -= BaseResidentOnFoodAccepted;
            _playerTriggerZone.PlayerEnterTriggerZone -= PlayerTriggerZoneOnPlayerEnterTriggerZone;
        }

        private void BaseResidentOnFoodAccepted(bool accepted)
        {
            if (accepted)
                ShowHint();
        }
        
        private void PlayerTriggerZoneOnPlayerEnterTriggerZone(Player.Player player, bool playerIn)
        {
            if (!playerIn)
                HideHint();
        }
    }
}