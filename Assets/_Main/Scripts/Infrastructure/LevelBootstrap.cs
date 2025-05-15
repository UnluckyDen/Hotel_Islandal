using _Main.Scripts.Infrastructure.Level;
using _Main.Scripts.Infrastructure.Tiles;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.Infrastructure
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private WayController _wayController;

        [SerializeField] private Player.Player _player;

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
            LevelTiles a = _levelBuilder.BuildLevel();
            
            _wayController.Init();
            _wayController.CollectWayPointsAtScene();
            _wayController.LinkWayPoints();
            
            a.ResidentsDistributor.Init();

            _player.Init(_wayController, InputService.Instance);
        }

        private void Destruct()
        { 
            //_residentsDistributor.Destruct();
            
            _player.Destruct();
        }
    }
}