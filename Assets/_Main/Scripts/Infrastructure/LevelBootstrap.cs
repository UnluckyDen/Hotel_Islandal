using _Main.Scripts.NPCs.Resident;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.Services;
using _Main.Scripts.Tutorial;
using UnityEngine;

namespace _Main.Scripts.Infrastructure
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private ResidentsDistributor _residentsDistributor;
        [SerializeField] private TutorialResidentsDistributor _tutorialResidentsDistributor;
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
            if (_residentsDistributor != null)
                _residentsDistributor.Init();
            if (_tutorialResidentsDistributor != null)
                _tutorialResidentsDistributor.Init();

            _player.Init(_wayController, InputService.Instance);
        }

        private void Destruct()
        {
            if (_residentsDistributor != null)
                _residentsDistributor.Destruct();
            if (_tutorialResidentsDistributor != null)
                _tutorialResidentsDistributor.Destruct();
            
            _player.Destruct();
        }
    }
}