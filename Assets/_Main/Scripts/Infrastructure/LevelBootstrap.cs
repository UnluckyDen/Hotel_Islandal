using _Main.Scripts.NPCs.Resident;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.Services;
using UnityEngine;

namespace _Main.Scripts.Infrastructure
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private ResidentsDistributor _residentsDistributor;
        [SerializeField] private WayController _wayController;

        [SerializeField] private Player.Player _player;

        private void Awake()
        {
            Init();
        }

        private void OnDestroy()
        {
            Destruct();
        }

        private void Init()
        {
            _residentsDistributor.Init();

            _player.Init(_wayController, InputService.Instance);
        }

        private void Destruct()
        {
            _residentsDistributor.Destruct();
            
            _player.Destruct();
        }
    }
}