using _Main.Scripts.Interfaces;

namespace _Main.Scripts.Cooking.Devices.Cooking
{
    public class TrashContainerCookingDevice : CookingDevice
    {
        public override bool IsEmpty => true;

        public override void PlaceMovableObject(IMovableObject movableObject)
        {
            if (movableObject == null || !movableObject.IsTrashable)
                return;
            
            Destroy(movableObject.transform.gameObject);
        }
    }
}