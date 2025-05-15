using _Main.Scripts.Infrastructure.Level;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevelSettingsSettings", menuName = "ScriptableObjects/ResidentCondition/LevelSettingsSettingsSettings", order = 1)]
    public class LevelSettings : ScriptableObject
    {
        [field: SerializeField] public int ResidentsWithOrders { get; private set; }
        [field: SerializeField] public int AggressiveResidents { get; private set; }
        [field: Space] 
        [field: SerializeField] public LevelTiles LevelTiles { get; private set; }
    }
}