using _Main.Scripts.Infrastructure.Level;
using _Main.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Main.Scripts.Infrastructure
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private Transform _levelRoot;

        private LevelTiles _levelTiles;

        public LevelTiles BuildLevel(LevelSettings levelSettings)
        {
            _levelTiles = Instantiate(levelSettings.LevelTiles, _levelRoot);

            return _levelTiles;
        }

        public void DestroyCurrentLevel()
        {
            if (_levelTiles != null)
                Destroy(_levelTiles.gameObject);
        }
    }
}