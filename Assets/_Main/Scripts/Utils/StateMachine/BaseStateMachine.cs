namespace _Main.Scripts.Utils.StateMachine
{
    public class BaseStateMachine
    {
        public IState CurrentState { get; private set; }

        public void UpdateStates() =>
            CurrentState?.Update();

        public void ToState(IState state)
        {
            CurrentState?.Dispose();
            CurrentState = state;
            state?.Enter();
        }

        public void Dispose() =>
            CurrentState?.Dispose();
    }
}