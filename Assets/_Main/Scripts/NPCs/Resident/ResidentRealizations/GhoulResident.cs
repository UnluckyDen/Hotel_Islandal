using _Main.Scripts.Cooking.Foods;

namespace _Main.Scripts.NPCs.Resident.ResidentRealizations
{
    public class GhoulResident : BaseResident
    {
        public override bool TryAcceptOrder(Food food)
        {
            return base.TryAcceptOrder(food);
        }
    }
}