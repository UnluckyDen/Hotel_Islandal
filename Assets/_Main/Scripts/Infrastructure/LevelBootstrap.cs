using _Main.Scripts.Infrastructure.Level;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.PortableDevices.Coins;
using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.Infrastructure
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private WayController _wayController; 
        [SerializeField] private WayPoint _startPoint;
        [SerializeField] private CoinReceiverMachine _coinReceiverMachine;

        [SerializeField] private Player.Player _player;

        private LevelTiles _levelTiles;

        private void Start()
        {
            Init();
        }

        private void OnDestroy()
        {
            Destruct();
        }

        private void Init()
        {
            _levelTiles = _levelBuilder.BuildLevel(_levelController.GetCurrentLevelSettings());
            _coinReceiverMachine.Init();
            
            _coinReceiverMachine.SetNeedCoins(_levelController.GetCurrentLevelSettings().CoinsToLeave);
            
            _wayController.CollectWayPointsAtScene(_levelTiles.WayPoints, _startPoint);
            _wayController.LinkWayPoints();
            
            _levelTiles.ResidentsDistributor.Init();

            _player.Init(_wayController, InputService.Instance);
        }

        private void Destruct()
        { 
            _levelTiles.ResidentsDistributor.Destruct();
            _coinReceiverMachine.Destruct();
            
            _player.Destruct();
        }
    }
}