using System;
using System.Collections.Generic;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.NPCs.Resident.Clues;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class VampireResident : BaseResident
    {
        [SerializeField] private List<BaseResidentClue> _residentClues;
        [SerializeField] private Transform _cluePlace;

        private List<BaseResidentClue> _baseResidentClues = new List<BaseResidentClue>();

        private void Start()
        {
            InstantiateClues();
        }

        public override void InstantiateClues()
        {
            foreach (var residentClue in _residentClues)
            {
                _baseResidentClues.Add(Instantiate(residentClue, _cluePlace.position, _cluePlace.rotation, _cluePlace));
            }
        }

        public override bool TryAcceptOrder(Food food)
        {
            return base.TryAcceptOrder(food);
        }
    }
}