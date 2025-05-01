using UnityEngine;

namespace _Main.Scripts.NPCs.Resident.Clues
{
    public class BaseResidentClue : MonoBehaviour
    {
        [SerializeField] private ResidentClueType _residentClueType;

        public ResidentClueType ResidentClueType => _residentClueType;
    }

    public enum ResidentClueType
    {
        AdditionClue = 1,
        ReplaceClue = 2
    }

    public enum ReplaceObject
    {
        Lamp = 1,
        Mirror = 2
    }
}