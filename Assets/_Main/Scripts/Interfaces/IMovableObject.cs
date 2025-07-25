namespace _Main.Scripts.Interfaces
{
    public interface IMovableObject : IHoverable, ITransformContains
    {
        public bool IsTrashable { get; }
        public void ToNonInteractive();
        public void ToInteractable();
    }
}