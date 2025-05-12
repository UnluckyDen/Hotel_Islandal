using _Main.Scripts.NPCs.Resident.ResidentRealizations;

namespace _Main.Scripts.Tutorial
{
    public class GiveOrderHint : BaseTutorialHint
    {
        private void Start()
        {
            BaseResident.FoodAccepted += BaseResidentOnFoodAccepted;
        }

        private void OnDestroy()
        {
            BaseResident.FoodAccepted -= BaseResidentOnFoodAccepted;
        }

        private void BaseResidentOnFoodAccepted(bool accepted)
        {
            if (accepted)
                ShowHint();
        }
    }
}