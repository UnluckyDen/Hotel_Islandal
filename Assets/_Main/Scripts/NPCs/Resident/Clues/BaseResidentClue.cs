using UnityEngine;

namespace _Main.Scripts.NPCs.Resident.Clues
{
    public class BaseResidentClue : MonoBehaviour
    {
        [SerializeField] private ResidentClueType _residentClueType;
        [SerializeField] private ReplaceObjectType _replaceObjectType;

        public ResidentClueType ResidentClueType => _residentClueType;
        public ReplaceObjectType ReplaceObjectType => _replaceObjectType;
    }

    public enum ResidentClueType
    {
        AdditionClue = 1,
        ReplaceClue = 2
    }

    public enum ReplaceObjectType
    {
        Lamp = 1,
        Mirror = 2
    }
}