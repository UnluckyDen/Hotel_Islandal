using _Main.Scripts.Interfaces;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class TrashContainerCookingDevice : CookingDevice
    {
        public override bool TryPlaceMovableObject(IMovableObject movableObject)
        {
            if (movableObject == null || !movableObject.IsTrashable)
                return false;
            
            Destroy(movableObject.transform.gameObject);
            return true;
        }
    }
}