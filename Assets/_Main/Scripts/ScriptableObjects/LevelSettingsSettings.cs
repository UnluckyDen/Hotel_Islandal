using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevelSettingsSettings", menuName = "ScriptableObjects/ResidentCondition/LevelSettingsSettingsSettings", order = 1)]
    public class LevelSettingsSettings : ScriptableObject
    {
        [field: SerializeField] public int ResidentsWithOrders { get; private set; }
        [field: SerializeField] public int AggressiveResidents { get; private set; }
    }
}