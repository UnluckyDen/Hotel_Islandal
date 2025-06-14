using System.Collections;
using _Main.Scripts.Environment.Elevators;
using _Main.Scripts.Infrastructure.Level;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.ScriptableObjects;
using _Main.Scripts.Services;
using _Main.Scripts.Utils.GlobalEvents.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventProvider = _Main.Scripts.Utils.GlobalEvents.EventProvider;

namespace _Main.Scripts.Infrastructure
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private WayController _wayController; 
        [SerializeField] private WayPoint _startPoint;
        [SerializeField] private Elevator _elevator;

        [SerializeField] private Player.Player _player;

        private LevelTiles _levelTiles;
        private Coroutine _changeLevelCoroutine;
        private Player.Player _currentPlayer;

        private void Start() => Init();

        private void OnDestroy() => Destruct();

        private void Init()
        {
            _elevator.Init();

            _currentPlayer = Instantiate(_player);

            ChangeLevel();
            
            _currentPlayer.Init(_wayController, InputService.Instance);
            _elevator.LevelConditionsComplete += ElevatorOnLevelConditionsComplete;
            
            EventProvider.Instance.Subscribe<PlayerLostMindEvent>(ForceReload);
        }

        private void Destruct()
        {
            _elevator.LevelConditionsComplete -= ElevatorOnLevelConditionsComplete;
            _levelTiles.ResidentsDistributor.Destruct();
            _elevator.Destruct();
            
            _currentPlayer.Destruct();

            EventProvider.Instance.UnSubscribe<PlayerLostMindEvent>(ForceReload);
            
            _currentPlayer.Destruct();
        }

        private void ChangeLevel()
        {
            if (_changeLevelCoroutine != null)
                return;
            
            _changeLevelCoroutine = StartCoroutine(ChangeLevelCoroutine());
        }

        private void LoadLevel()
        {
            LevelSettings currentLevelSettings = _levelController.GetCurrentLevelSettings();
            _levelTiles = _levelBuilder.BuildLevel(currentLevelSettings);
            _elevator.StartLevel(currentLevelSettings.CoinsToLeave, _levelController.CurrentLevel);
            
            _wayController.CollectWayPoints(_levelTiles.WayPoints, _startPoint);
            _wayController.LinkWayPoints();

            _levelTiles.ResidentsDistributor.Init(currentLevelSettings.ResidentsAtLevel,
                currentLevelSettings.AggressiveResidents, currentLevelSettings.HaveOrderResidents);
        }

        public void UnloadLevel()
        {
            _levelTiles?.ResidentsDistributor.Destruct();

            _wayController.DestroyCurrentPaths();
            _levelBuilder.DestroyCurrentLevel();
        }

        private void ElevatorOnLevelConditionsComplete() =>
            ChangeLevel();

        private IEnumerator ChangeLevelCoroutine()
        {
            int newLevelNumber = _levelController.CurrentLevel;
            
            if (_levelTiles != null)
                newLevelNumber++;
            
            UnloadLevel();
            _levelController.UpdateCurrentLevel(newLevelNumber);
            LoadLevel();
            
            yield break;
        }

        private void ForceReload(PlayerLostMindEvent playerLostMindEvent)
        {
            _levelController.UpdateCurrentLevel(0);
            SceneManager.LoadScene("MainScene");
        }
    }
}