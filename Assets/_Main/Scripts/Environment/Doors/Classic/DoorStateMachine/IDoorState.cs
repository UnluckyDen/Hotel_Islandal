using _Main.Scripts.Utils.StateMachine;

namespace _Main.Scripts.Environment.Doors.Classic.DoorStateMachine
{
    public interface IDoorState : IState
    {
        public void OnClick();
    }
}