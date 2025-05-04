
namespace _Main.Scripts.Interfaces
{
    public interface IObjectPlace
    {
        public IMovableObject CurrentMovableObject { get; }
        public bool CanApply(IMovableObject movableObject);
        public void PlaceMovableObject(IMovableObject movableObject);
        public IMovableObject TakeMovableObject();
    }
}