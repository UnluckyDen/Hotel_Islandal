using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Environment;
using _Main.Scripts.Environment.Doors.ResidentDoor;
using _Main.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class BaseResident : MonoBehaviour
    {
        [SerializeField] private ResidentOrderSettings _residentOrderSettings;
        [SerializeField] private ResidentConditionHintSettings _residentConditionHintSettings;

        [SerializeField] private Food _orderedFood;
        
        private ResidentDoor _residentDoor;
        private PlayerTriggerZone _playerTriggerZone;

        private ConditionHintPair _conditionHintPair;
        private OrderMethodPair _orderMethodPair;

        private Transform _hintPlace;
        private GameObject _tempConditionGameObject;
        private GameObject _tempOrderGameObject;

        private bool _playerInZone;

        public Transform CluePlace { get; private set; }

        public virtual void Init(ResidentDoor residentDoor, PlayerTriggerZone playerTriggerZone, Transform hintPlace, Transform cluesPlace)
        {
            _residentDoor = residentDoor;
            _playerTriggerZone = playerTriggerZone;
            _hintPlace = hintPlace;
            CluePlace = cluesPlace;
            
            _playerTriggerZone.PlayerEnterTriggerZone += PlayerTriggerZoneOnPlayerEnterTriggerZone;
            GenerateCondition();
            
            if (_conditionHintPair.Condition == ResidentConditionType.HaveOrder)
                CreateOrder();
        }

        public void Destruct()
        {
            _playerTriggerZone.PlayerEnterTriggerZone -= PlayerTriggerZoneOnPlayerEnterTriggerZone;
        }

        public virtual bool TryAcceptOrder(Food food)
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

        public virtual void HandleKnock()
        {
            if (_conditionHintPair.Condition == ResidentConditionType.HaveOrder && _playerInZone)
            {
                _residentDoor.OpenDoor();
                DestroyConditionHint();
                MakeOrder();
            }
        }

        protected virtual void GenerateCondition()
        {
            _conditionHintPair = _residentConditionHintSettings.GetRandomPair();
        }

        protected virtual void CreateOrder()
        {
            _orderMethodPair = _residentOrderSettings.GetRandomPair();
            _orderedFood = _orderMethodPair.Food;
        }

        private void MakeOrder()
        {
            _tempOrderGameObject = Instantiate(_orderMethodPair.Method, _hintPlace);
        }

        private void DestroyOrderHint()
        {
            if (_tempOrderGameObject != null)
                Destroy(_tempOrderGameObject.gameObject);
        }

        private void ShowConditionHint()
        {
            _tempConditionGameObject = Instantiate(_conditionHintPair.Method, _hintPlace);
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