using _Main.Scripts.Utils.StateMachine;

namespace _Main.Scripts.Environment.Doors.StateMachine
{
    public interface IDoorState : IState
    {
        public void OnClick();
    }
}