using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.NPCs.Resident;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Main.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewResidentConditionHintSettings", menuName = "ScriptableObjects/ResidentCondition/ResidentConditionHintSettings", order = 1)]
    public class ResidentConditionHintSettings : ScriptableObject
    {
        [field: SerializeField] public List<ConditionHintPair> ConditionPairs { get; private set; }

        public ConditionHintPair GetPair(ResidentConditionType condition) =>
            ConditionPairs.FirstOrDefault(p => p.Condition == condition);

        public ConditionHintPair GetRandomPair() =>
            ConditionPairs[Random.Range(0, ConditionPairs.Count)];
    }
    
    [Serializable]
    public struct ConditionHintPair
    {
        public ResidentConditionType Condition;
        public GameObject Method;
    }
}