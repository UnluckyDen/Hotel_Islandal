namespace _Main.Scripts.Utils.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Update();
        public void Dispose();
    }
}