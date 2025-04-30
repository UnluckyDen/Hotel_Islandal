using UnityEngine;

namespace _Main.Scripts.NPCs.Resident.Clues
{
    public class BaseResidentClue : MonoBehaviour
    {
        [SerializeField] private ResidentClueType _residentClueType;
        
    }

    public enum ResidentClueType
    {
        AdditionClue = 1,
        ReplaceClue = 2
    }
}