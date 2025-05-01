using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.Environment;
using _Main.Scripts.Environment.Doors.ResidentDoor;
using _Main.Scripts.NPCs.Resident.Clues;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident.ResidentRealizations
{
    public class VampireResident : BaseResident
    {
        [SerializeField] private List<BaseResidentClue> _residentClues;

        private List<BaseResidentClue> _baseResidentClues = new List<BaseResidentClue>();
        
        public override void Init(ResidentDoor residentDoor, PlayerTriggerZone playerTriggerZone, Transform orderPlace,
            Transform cluesPlace)
        {
            base.Init(residentDoor, playerTriggerZone, orderPlace, cluesPlace);
            
            foreach (var residentClue in _residentClues)
            {
                _baseResidentClues.Add(Instantiate(residentClue, CluePlace.position, CluePlace.rotation, CluePlace));
            }
        }

        public override bool TryAcceptOrder(Food food)
        {
            return base.TryAcceptOrder(food);
        }
    }
}