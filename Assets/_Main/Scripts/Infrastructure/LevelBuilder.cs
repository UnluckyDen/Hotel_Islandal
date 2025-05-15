using _Main.Scripts.Infrastructure.Level;
using _Main.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Main.Scripts.Infrastructure
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private LevelSettings _levelSettings;
        [SerializeField] private Transform _levelRoot;

        private LevelTiles _levelTiles;

        public LevelTiles BuildLevel() =>
            _levelTiles = Instantiate(_levelSettings.LevelTiles, _levelRoot);
    }
}