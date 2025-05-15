using System.Collections.Generic;
using _Main.Scripts.Infrastructure.Level;
using _Main.Scripts.NPCs.Resident.ResidentRealizations;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevelSettingsSettings", menuName = "ScriptableObjects/ResidentCondition/LevelSettingsSettingsSettings", order = 1)]
    public class LevelSettings : ScriptableObject
    {
        [field: SerializeField] public List<BaseResident> ResidentsAtLevel { get; private set; }
        [field: SerializeField] public int AggressiveResidents { get; private set; }
        [field: SerializeField] public int HaveOrderResidents { get; private set; }
        [field: SerializeField] public int CoinsToLeave { get; private set; }
        [field: SerializeField] public LevelTiles LevelTiles { get; private set; }
    }
}