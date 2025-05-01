using System.Collections.Generic;
using _Main.Scripts.NPCs.Resident.Clues;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class ResidentCluesController : MonoBehaviour
    {
        [SerializeField] private Transform _cluePlace;

        private List<BaseResidentClue> _baseResidentClues = new();
        
        public void Init()
        {
            
        }

        public void Destruct()
        {
            
        }

        public void InstantiateClue(BaseResidentClue residentClue)
        {
            var clue = Instantiate(residentClue, _cluePlace.position, _cluePlace.rotation, _cluePlace);
            if (clue.ResidentClueType == ResidentClueType.ReplaceClue)
            {
                
            }
            
            _baseResidentClues.Add(clue);
        }
        
        public void InstantiateClue(List<BaseResidentClue> residentClues)
        {
            foreach (var residentClue in residentClues)
                InstantiateClue(residentClue);
        }
    }
}