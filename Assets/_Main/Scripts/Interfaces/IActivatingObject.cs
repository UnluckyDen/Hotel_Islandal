namespace _Main.Scripts.Interfaces
{
    public interface IActivatingObject
    {
        public bool CurrentActivity { get; }
        public void Activate();
        public void Deactivate();
        public void SwitchActive();
    }
}