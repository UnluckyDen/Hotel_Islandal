using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.NPCs.Resident.ResidentRealizations;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class ResidentsDistributor : MonoBehaviour
    {
        [SerializeField] private List<BaseResident> _residents;
        [SerializeField] private List<ResidentRoom> _residentRooms;
        [SerializeField] private Transform _startPoint;
        
        public void Init()
        {
            foreach (ResidentRoom residentRoom in _residentRooms)
            {
                BaseResident randomResident = InstantiateRandomResident(residentRoom.ResidentPlace);
                residentRoom.Init(randomResident, _residentRooms.IndexOf(residentRoom) + 1);
            }
        }

        public void Destruct()
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
    }
}