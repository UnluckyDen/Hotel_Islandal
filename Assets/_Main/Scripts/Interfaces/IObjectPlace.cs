
namespace _Main.Scripts.Interfaces
{
    public interface IObjectPlace
    {
        public bool IsEmpty { get; }
        public bool MayContainMultipleObjects { get; }
        public void PlaceMovableObject(IMovableObject movableObject);
        public IMovableObject TakeMovableObject();
    }
}