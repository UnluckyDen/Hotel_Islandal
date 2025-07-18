using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.NPCs.Resident;
using _Main.Scripts.NPCs.Resident.ResidentRealizations;
using UnityEngine;

namespace _Main.Scripts.Tutorial
{
    public class TutorialResidentsDistributor : ResidentsDistributor
    {
        [Space]
        [SerializeField] private List<TutorialResidentContext> _tutorialResidentContexts;
        
        public override void Init(List<BaseResident> residents, int aggressiveResidents, int haveOrderResident)
        {
            for (var index = 0; index < _residentRooms.Count; index++)
            {
                var residentRoom = _residentRooms[index];
                BaseResident randomResident = InstantiateResident(residentRoom.ResidentPlace, _tutorialResidentContexts[index].Resident);
                randomResident.SetContext(_tutorialResidentContexts[index].ResidentCondition, _tutorialResidentContexts[index].OrderedFood);
                residentRoom.Init(randomResident, _residentRooms.IndexOf(residentRoom) + 1);
            }
        }

        public override void Destruct()
        {
            foreach (ResidentRoom residentRoom in _residentRooms)
                residentRoom.Destruct();
        }

        private BaseResident InstantiateResident(Transform parent, BaseResident baseResident)
        {
            var resident = Instantiate(baseResident, parent.position, parent.rotation, parent);
            return resident;
        }

        [ContextMenu("Sort rooms")]
        private void SortRooms()
        {
            _residentRooms = _residentRooms.OrderBy(residentRoom => 
                Vector3.Distance(_startPoint.position, residentRoom.transform.position)).ToList();

        }
    }
    
    [Serializable]
    public struct TutorialResidentContext
    {
        public BaseResident Resident;
        public ResidentConditionType ResidentCondition;
        public Food OrderedFood;
    }
}