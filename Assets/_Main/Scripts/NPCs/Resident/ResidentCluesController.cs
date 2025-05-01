using System;
using System.Collections.Generic;
using _Main.Scripts.NPCs.Resident.Clues;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class ResidentCluesController : MonoBehaviour
    {
        [SerializeField] private Transform _cluePlace;
        [SerializeField] private List<ReplaceObjectTypePair> _replaceObjectsList;

        private Dictionary<ReplaceObjectType, GameObject> _replaceObjects;
        
        private List<BaseResidentClue> _baseResidentClues = new();
        
        public void Init()
        {
            _replaceObjects = new Dictionary<ReplaceObjectType, GameObject>();

            foreach (var pair in _replaceObjectsList)
                _replaceObjects.Add(pair.ReplaceObjectType, pair.ReplaceGameObject);
        }

        public void Destruct()
        {
            
        }

        public void InstantiateClue(BaseResidentClue residentClue)
        {
            var clue = Instantiate(residentClue, _cluePlace.position, _cluePlace.rotation, _cluePlace);
            if (clue.ResidentClueType == ResidentClueType.ReplaceClue 
                && _replaceObjects.TryGetValue(clue.ReplaceObjectType, out var replaceObject)) 
                Destroy(replaceObject.gameObject);
            
            _baseResidentClues.Add(clue);
        }
        
        public void InstantiateClue(List<BaseResidentClue> residentClues)
        {
            foreach (var residentClue in residentClues)
                InstantiateClue(residentClue);
        }
    }

    [Serializable]
    public struct ReplaceObjectTypePair
    {
        public ReplaceObjectType ReplaceObjectType;
        public GameObject ReplaceGameObject;
    }
}