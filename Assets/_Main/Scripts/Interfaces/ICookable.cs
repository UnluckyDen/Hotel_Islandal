namespace _Main.Scripts.Interfaces
{
    public interface ICookable
    {
        public float CurrentCookingTime { get; }

        public void AddCookingTime(float time);
    }
}