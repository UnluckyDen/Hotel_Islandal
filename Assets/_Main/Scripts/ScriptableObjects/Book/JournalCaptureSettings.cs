using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects.Book
{
    [CreateAssetMenu(fileName = "NewJournalCaptureSettings", menuName = "ScriptableObjects/Book/JournalCaptureSettings", order = 1)]
    public class JournalCaptureSettings : ScriptableObject
    {
        [field: SerializeField] public List<JournalPageSettings> JournalPageSettings { get; private set; }
    }
}