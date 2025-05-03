using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Environment;
using _Main.Scripts.Environment.Doors.ResidentDoor;
using _Main.Scripts.NPCs.Resident.Clues;
using _Main.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident.ResidentRealizations
{
    public class BaseResident : MonoBehaviour
    {
        [SerializeField] private ResidentConditionHintSettings _residentConditionHintSettings;
        [SerializeField] private List<BaseResidentClue> _residentClues;
        [SerializeField] private ResidentOrderSettings _residentOrderSettings;
        [SerializeField] private ResidentSound _residentSound;

        [SerializeField] private Food _orderedFood;
        
        private ResidentDoor _residentDoor;
        private PlayerTriggerZone _playerTriggerZone;
        private ResidentCluesController _residentCluesController;
        
        private ResidentConditionType _currentCondition;
        
        private bool _playerInZone;
        
        public virtual void Init(ResidentDoor residentDoor, PlayerTriggerZone playerTriggerZone, ResidentCluesController residentCluesController)
        {
            _residentDoor = residentDoor;
            _playerTriggerZone = playerTriggerZone;
            _residentCluesController = residentCluesController;
            
            _playerTriggerZone.PlayerEnterTriggerZone += PlayerTriggerZoneOnPlayerEnterTriggerZone;
            GenerateCondition();
            
            if (_currentCondition == ResidentConditionType.HaveOrder)
                CreateOrder();
            
            _residentCluesController.InstantiateClue(_residentClues);
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
                _currentCondition = ResidentConditionType.NonActive;
                
                return true;
            }

            Debug.Log("In not ordered food");
            return false;
        }

        public virtual void HandleKnock()
        {
            if (_currentCondition == ResidentConditionType.HaveOrder && _playerInZone)
            {
                _residentDoor.OpenDoor();
                _residentSound.MakeOrder(_orderedFood);
            }
        }

        protected virtual void GenerateCondition()
        {
            _currentCondition = _residentConditionHintSettings.GetRandomPair().Condition;
        }

        protected virtual void CreateOrder()
        {
            _orderedFood = _residentOrderSettings.GetRandomPair().Food;
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
            _residentSound.ShowConditionHint(_currentCondition);
        }

        private void OnPlayerZoneOut()
        {
            _playerInZone = false;
            _residentDoor.CloseDoor();
        }
    }
}