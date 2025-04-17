using _Main.Scripts.Cooking.Foods;
using UnityEngine;

namespace _Main.Scripts.NPCs.Resident
{
    public class BaseResident : MonoBehaviour
    {
        [SerializeField] private Food _order;

        private void CreateOrder()
        {
            
        }

        private void MakeOrder()
        {
            
        }

        private bool TryAcceptOrder()
        {
            return true;
        }
    }
}