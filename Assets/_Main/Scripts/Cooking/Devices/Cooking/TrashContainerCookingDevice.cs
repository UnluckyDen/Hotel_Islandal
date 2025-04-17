using _Main.Scripts.Interfaces;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class TrashContainerCookingDevice : CookingDevice
    {
        public override bool IsEmpty => true;
        
        public override bool PlaceMovableObject(IMovableObject movableObject)
        {
            if (movableObject == null || !movableObject.IsTrashable)
                return false;
            
            Destroy(movableObject.transform.gameObject);
            return true;
        }
    }
}