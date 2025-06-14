using System;
using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.Analytics;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Environment;
using _Main.Scripts.Environment.Doors.ResidentDoor;
using _Main.Scripts.NPCs.Resident.Clues;
using _Main.Scripts.NPCs.Resident.Screamers;
using _Main.Scripts.PortableDevices.Coins;
using _Main.Scripts.ScriptableObjects;
using Unity.Mathematics;
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
        [SerializeField] private Coin _coinPrefab;

        private Food _orderedFood;
        
        private ResidentDoor _residentDoor;
        private PlayerTriggerZone _playerTriggerZone;
        private ResidentCluesController _residentCluesController;
        private ResidentScreamer _residentScreamer;
        
        private ResidentConditionType _currentCondition;
        
        private Player.Player _player;
        private bool _playerOpenDoor;
        private const float ResidentAwaitOrderTime = 2f;

        public virtual void SetContext(ResidentConditionType condition, Food orderedFood)
        {
            _currentCondition = condition;
            
            if (_currentCondition == ResidentConditionType.HaveOrder)
                _orderedFood = orderedFood;
        }
        
        public virtual void Init(ResidentDoor residentDoor, PlayerTriggerZone playerTriggerZone, ResidentCluesController residentCluesController, ResidentScreamer residentScreamer)
        {
            _residentDoor = residentDoor;
            _playerTriggerZone = playerTriggerZone;
            _residentCluesController = residentCluesController;
            _residentScreamer = residentScreamer;
            
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
                GiveCoinToPlayer();
                _residentDoor.CloseDoor();
                _currentCondition = ResidentConditionType.NonActive;
                
                return true;
            }

            Debug.Log("In not ordered food");
            FoodAccepted?.Invoke(false);
            ShowScreamer(_player);
            return false;
        }

        public virtual void HandleKnock()
        {
            _playerOpenDoor = true;
            
            GlobalAnalyticsService.Instance.SendCustomEvent(new DoorInteractionAnalyticsEvent(_currentCondition));

            switch (_currentCondition)
            {
                case ResidentConditionType.HaveOrder when _player != null:
                    _residentDoor.OpenDoor();
                    _residentSound.MakeOrder(_orderedFood, AwaitOrder);
                    return;
                
                case ResidentConditionType.Aggressive when _player != null:
                    ShowScreamer(_player);
                    break;
                
                case ResidentConditionType.NonActive:
                    break;
            }
        }
        
        protected virtual void ShowScreamer(Player.Player player) =>
            _residentScreamer.Scream(player);

        protected void OnPlayerLeaveHaveOrderResident(Player.Player player)
        {
            if (!_playerOpenDoor)
                ShowScreamer(player);
        }

        protected void GiveCoinToPlayer()
        {
            Coin coin = Instantiate(_coinPrefab, transform.position, quaternion.identity);
            coin.FlyToStash(_player.CoinStash);
        }

        private void OnPlayerZoneIn(Player.Player player)
        {
            _player = player;
            _residentSound.ShowConditionHint(_currentCondition);
        }

        private void OnPlayerZoneOut(Player.Player player)
        {
            _player = null;
            _residentDoor.CloseDoor();
            
            if (_currentCondition == ResidentConditionType.HaveOrder)
                OnPlayerLeaveHaveOrderResident(player);
        }

        private void PlayerTriggerZoneOnPlayerEnterTriggerZone(Player.Player player, bool playerIn)
        {
            if (playerIn)
            {
                OnPlayerZoneIn(player);
                return;
            }
            
            OnPlayerZoneOut(player);
        }

        private void AwaitOrder() => 
            StartCoroutine(AwaitOrderCoroutine());

        private IEnumerator AwaitOrderCoroutine()
        {
            yield return new WaitForSeconds(ResidentAwaitOrderTime);
            _residentDoor.CloseDoor();
        }
    }
}