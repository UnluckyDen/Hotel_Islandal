using System.Collections.Generic;
using _Main.Scripts.UI.Book.BookPages;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects.Book
{
    [CreateAssetMenu(fileName = "NewJournalPageSettings", menuName = "ScriptableObjects/Book/JournalPageSettings", order = 1)]
    public class JournalPageSettings : ScriptableObject
    {
        [field: SerializeField] public DefaultBookPage BookPage { get; private set; }
        [field: SerializeField] public List<JournalPageContentElementSettings> PageContentElementSettingsList;
    }
}