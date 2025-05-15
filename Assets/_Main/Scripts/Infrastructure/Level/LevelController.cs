using System.Collections.Generic;
using _Main.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Main.Scripts.Infrastructure.Level
{
    public class LevelController : MonoBehaviour
    {
        private const string CurrentLevelStringName = "current_level";

        [SerializeField] private List<LevelSettings> _levelSettings;

        public int CurrentLevel
        {
            get => PlayerPrefs.GetInt(CurrentLevelStringName); 
            private set => PlayerPrefs.SetInt(CurrentLevelStringName, value);
        }

        public void UpdateCurrentLevel(int level)
        {
            if (level < _levelSettings.Count)
                CurrentLevel = level;
        }

        public LevelSettings GetCurrentLevelSettings() => _levelSettings[CurrentLevel];

        [ContextMenu("ClearProgress")]
        public void ClearProgress()
        {
            CurrentLevel = 0;
        }
    }
}