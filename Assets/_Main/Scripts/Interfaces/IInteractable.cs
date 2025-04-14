namespace _Main.Scripts.Interfaces
{
    public interface IInteractable : IHoverable, ITransformContains
    {
        public void OnClick();
    }
}