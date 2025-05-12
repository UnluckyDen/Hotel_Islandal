using System;
using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Environment;
using _Main.Scripts.Environment.Doors.ResidentDoor;
using _Main.Scripts.NPCs.Resident.Clues;
using _Main.Scripts.ScriptableObjects;
using _Main.Scripts.UI;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident.ResidentRealizations
{
    public class BaseResident : MonoBehaviour
    {
        public static event Action<bool> FoodAccepted;
        
        [SerializeField] private ResidentConditionHintSettings _residentConditionHintSettings;
        [SerializeField] private ResidentOrderSettings _residentOrderSettings;
        
        [SerializeField] private ResidentSoundsSettings _residentSoundsSettings;
        [SerializeField] private List<BaseResidentClue> _residentClues;
        [SerializeField] private ResidentSound _residentSound;

        [SerializeField] private Food _orderedFood;
        
        private ResidentDoor _residentDoor;
        private PlayerTriggerZone _playerTriggerZone;
        private ResidentCluesController _residentCluesController;
        
        private ResidentConditionType _currentCondition;
        
        private bool _playerInZone;
        private bool _playerOpenDoor;

        public virtual void SetContext(ResidentConditionType condition, Food orderedFood)
        {
            _currentCondition = condition;
            
            if (_currentCondition == ResidentConditionType.HaveOrder)
                _orderedFood = orderedFood;
        }
        
        public virtual void Init(ResidentDoor residentDoor, PlayerTriggerZone playerTriggerZone, ResidentCluesController residentCluesController)
        {
            _residentDoor = residentDoor;
            _playerTriggerZone = playerTriggerZone;
            _residentCluesController = residentCluesController;
            
            _residentSound.Init(_residentSoundsSettings);
            
            _playerTriggerZone.PlayerEnterTriggerZone += PlayerTriggerZoneOnPlayerEnterTriggerZone;
            
            _residentCluesController.InstantiateClue(_residentClues);
        }

        public void Destruct()
        {
            _residentSound.Destruct();
            
            _playerTriggerZone.PlayerEnterTriggerZone -= PlayerTriggerZoneOnPlayerEnterTriggerZone;
        }

        public virtual bool TryAcceptOrder(Food food)
        {
            if (food.GetType() == _orderedFood.GetType())
            {
                Debug.Log("Is ordered food");
                FoodAccepted?.Invoke(true);
                _residentDoor.CloseDoor();
                _currentCondition = ResidentConditionType.NonActive;
                
                return true;
            }

            Debug.Log("In not ordered food");
            FoodAccepted?.Invoke(false);
            WindowController.Instance.ShowScreamerWindow();
            return false;
        }

        public virtual void HandleKnock()
        {
            _playerOpenDoor = true;
            
            switch (_currentCondition)
            {
                case ResidentConditionType.HaveOrder when _playerInZone:
                    _residentDoor.OpenDoor();
                    _residentSound.MakeOrder(_orderedFood);
                    return;
                
                case ResidentConditionType.Aggressive when _playerInZone:
                    ShowScreamer();
                    break;
                
                case ResidentConditionType.NonActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        protected virtual void ShowScreamer()
        {
            WindowController.Instance.ShowScreamerWindow();
        }

        protected void OnPlayerLeaveHaveOrderResident()
        {
            if (!_playerOpenDoor)
                ShowScreamer();
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
            
            if (_currentCondition == ResidentConditionType.HaveOrder)
                OnPlayerLeaveHaveOrderResident();
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
    }
}