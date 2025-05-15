using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cooking.Foods;
using _Main.Scripts.NPCs.Resident.ResidentRealizations;
using _Main.Scripts.ScriptableObjects;
using _Main.Scripts.Utils;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class ResidentsDistributor : MonoBehaviour
    {
        [SerializeField] protected List<ResidentRoom> _residentRooms;
        [SerializeField] protected Transform _startPoint;
        [Space] 
        [SerializeField] private ResidentOrderSettings _residentOrderSettings;

        private int _aggressiveResidents;
        private int _haveOrderResident;
        
        private List<BaseResident> _residents;

        public virtual void Init(List<BaseResident> residents, int aggressiveResidents, int haveOrderResident)
        {
            _residents = residents;
            _aggressiveResidents = aggressiveResidents;
            _haveOrderResident = haveOrderResident;
            
            var residentContexts = GenerateResidentsContext();

            for (var index = 0; index < _residentRooms.Count; index++)
            {
                var residentRoom = _residentRooms[index];
                BaseResident randomResident = InstantiateRandomResident(residentRoom.ResidentPlace);
                randomResident.SetContext(residentContexts[index].ResidentCondition, residentContexts[index].OrderedFood);
                residentRoom.Init(randomResident, _residentRooms.IndexOf(residentRoom) + 1);
            }
        }

        public virtual void Destruct()
        {
            foreach (ResidentRoom residentRoom in _residentRooms)
                residentRoom.Destruct();
        }

        private BaseResident InstantiateRandomResident(Transform parent)
        {
            var randomResident = _residents[Random.Range(0, _residents.Count)];

            var resident = Instantiate(randomResident, parent.position, parent.rotation, parent);
            return resident;
        }

        [ContextMenu("Sort rooms")]
        private void SortRooms()
        {
            _residentRooms = _residentRooms.OrderBy(residentRoom => 
                Vector3.Distance(_startPoint.position, residentRoom.transform.position)).ToList();

        }

        private List<ResidentContext> GenerateResidentsContext()
        {
            var residentContexts = new List<ResidentContext>();

            var conditionQueue = new Queue<ResidentConditionType>();

            for (int i = 0; i < _haveOrderResident; i++)
                conditionQueue.Enqueue(ResidentConditionType.HaveOrder);
            
            for (int i = 0; i < _aggressiveResidents; i++)
                conditionQueue.Enqueue(ResidentConditionType.Aggressive);

            for (int i = 0; i < _residentRooms.Count; i++)
            {
                ResidentConditionType residentCondition = conditionQueue.Count != 0 
                    ? conditionQueue.Dequeue() 
                    : ResidentConditionType.NonActive;
                
                residentContexts.Add(new ResidentContext(residentCondition,
                    _residentOrderSettings.GetRandomPair().Food));
            }

            residentContexts.RandomShuffleListElements();

            return residentContexts;
        }
    }

    public struct ResidentContext
    {
        public readonly ResidentConditionType ResidentCondition;
        public readonly Food OrderedFood;

        public ResidentContext(ResidentConditionType residentCondition, Food orderedFood)
        {
            ResidentCondition = residentCondition;
            OrderedFood = orderedFood;
        }
    }
}