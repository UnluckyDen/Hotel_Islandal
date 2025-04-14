namespace _Main.Scripts.Interfaces
{
    public interface IMovableObject : IHoverable, ITransformContains
    {
        public void ToNonInteractive();
        public void ToInteractable();
    }
}