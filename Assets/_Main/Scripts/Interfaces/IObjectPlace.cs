
namespace _Main.Scripts.Interfaces
{
    public interface IObjectPlace
    {
        public bool TryPlaceMovableObject(IMovableObject movableObject);
        public IMovableObject TryTakeMovableObject();
    }
}