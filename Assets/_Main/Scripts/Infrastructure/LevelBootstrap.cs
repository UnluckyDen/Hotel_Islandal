using System.Collections;
using _Main.Scripts.Environment.Elevators;
using _Main.Scripts.Infrastructure.Level;
using _Main.Scripts.Player.Movement.Way;
using _Main.Scripts.ScriptableObjects;
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
        [SerializeField] private Elevator _elevator;

        [SerializeField] private Player.Player _player;

        private LevelTiles _levelTiles;
        private Coroutine _changeLevelCoroutine;

        private void Start() => Init();

        private void OnDestroy() => Destruct();

        private void Init()
        {
            _elevator.Init();

            ChangeLevel();
            
            _player.Init(_wayController, InputService.Instance);
            _elevator.LevelConditionsComplete += ElevatorOnLevelConditionsComplete;
        }

        private void Destruct()
        { 
            _elevator.LevelConditionsComplete -= ElevatorOnLevelConditionsComplete;
            _levelTiles.ResidentsDistributor.Destruct();
            _elevator.Destruct();
            
            _player.Destruct();
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
            
            _levelBuilder.DestroyCurrentLevel();
            _wayController.DestroyCurrentPaths();
        }

        private void ElevatorOnLevelConditionsComplete()
        {
            ChangeLevel();
        }

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
    }
}