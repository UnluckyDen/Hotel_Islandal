using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewResidentSoundsSettings", menuName = "ScriptableObjects/ResidentCondition/ResidentSoundsSettings", order = 1)]
    public class ResidentSoundsSettings : ScriptableObject
    {
        [field: SerializeField] public List<AudioClip> LongSounds { get; private set; }
        [field: SerializeField] public List<AudioClip> ShortSounds { get; private set; }
    }
}