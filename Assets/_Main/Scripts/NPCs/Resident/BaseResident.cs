using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Environment;
using _Main.Scripts.Environment.Doors.ResidentDoor;
using _Main.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class BaseResident : MonoBehaviour
    {
        [SerializeField] private ResidentDoor _residentDoor;
        [SerializeField] private PlayerTriggerZone _playerTriggerZone;
        [SerializeField] private ResidentOrderSettings _residentOrderSettings;
        [SerializeField] private ResidentConditionHintSettings _residentConditionHintSettings;
        [SerializeField] private Transform _orderPlace;

        [SerializeField] private Food _orderedFood;

        private ConditionHintPair _conditionHintPair;
        private OrderMethodPair _orderMethodPair;

        private GameObject _tempConditionGameObject;
        private GameObject _tempOrderGameObject;

        private bool _playerInZone;

        private void Awake()
        {
            _playerTriggerZone.PlayerEnterTriggerZone += PlayerTriggerZoneOnPlayerEnterTriggerZone;
            GenerateCondition();
            
            if (_conditionHintPair.Condition == ResidentConditionType.HaveOrder)
                CreateOrder();
        }

        private void OnDestroy()
        {
            _playerTriggerZone.PlayerEnterTriggerZone -= PlayerTriggerZoneOnPlayerEnterTriggerZone;
        }

        public bool TryAcceptOrder(Food food)
        {
            if (food.GetType() == _orderedFood.GetType())
            {
                Debug.Log("Is ordered food");
                _residentDoor.CloseDoor();
                _conditionHintPair = _residentConditionHintSettings.GetPair(ResidentConditionType.NonActive);
                DestroyOrderHint();
                
                return true;
            }

            Debug.Log("In not ordered food");
            return false;
        }

        public void HandleKnock()
        {
            if (_conditionHintPair.Condition == ResidentConditionType.HaveOrder && _playerInZone)
            {
                _residentDoor.OpenDoor();
                DestroyConditionHint();
                MakeOrder();
            }
        }

        private void GenerateCondition()
        {
            _conditionHintPair = _residentConditionHintSettings.GetRandomPair();
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

        private void ShowConditionHint()
        {
            _tempConditionGameObject = Instantiate(_conditionHintPair.Method, _orderPlace);
        }

        private void DestroyConditionHint()
        {
            if (_tempConditionGameObject != null)
                Destroy(_tempConditionGameObject.gameObject);
        }

        private void PlayerTriggerZoneOnPlayerEnterTriggerZone(Player.Player player, bool playerIn)
        {
            if (playerIn)
            {
                OnPlayerZoneIn();
                return;
            }
            
            OnPlayerZoneOut();
        }

        private void OnPlayerZoneIn()
        {
            _playerInZone = true;
            ShowConditionHint();
        }

        private void OnPlayerZoneOut()
        {
            _playerInZone = false;
            _residentDoor.CloseDoor();
            DestroyConditionHint();
            DestroyOrderHint();
        }
    }
}