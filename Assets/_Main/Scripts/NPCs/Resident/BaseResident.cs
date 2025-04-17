using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Environment;
using _Main.Scripts.Environment.Doors;
using _Main.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class BaseResident : MonoBehaviour
    {
        [SerializeField] private Door _residentDoor;
        [SerializeField] private PlayerTriggerZone _playerTriggerZone;
        [SerializeField] private ResidentOrderSettings _residentOrderSettings;
        [SerializeField] private Transform _orderPlace;

        [SerializeField] private Food _orderedFood;

        private OrderMethodPair _orderMethodPair;
        private GameObject _tempOrderGameObject;

        private void Awake()
        {
            _playerTriggerZone.PlayerEnterTriggerZone += PlayerTriggerZoneOnPlayerEnterTriggerZone;
            CreateOrder();
        }

        private void OnDestroy()
        {
            _playerTriggerZone.PlayerEnterTriggerZone -= PlayerTriggerZoneOnPlayerEnterTriggerZone;
        }

        private void CreateOrder()
        {
            _orderMethodPair = _residentOrderSettings.GetRandomPair();
            _orderedFood = _orderMethodPair.Food;
        }

        private void MakeOrder()
        {
            _tempOrderGameObject = Instantiate(_orderMethodPair.Method, _orderPlace);
        }

        private void DestroyOrderHint()
        {
            if (_tempOrderGameObject != null)
                Destroy(_tempOrderGameObject.gameObject);
        }

        private bool TryAcceptOrder()
        {
            return true;
        }

        private void PlayerTriggerZoneOnPlayerEnterTriggerZone(Player.Player player, bool playerIn)
        {
            if (playerIn)
                MakeOrder();
            
            if (!playerIn)
                DestroyOrderHint();
        }
    }
}