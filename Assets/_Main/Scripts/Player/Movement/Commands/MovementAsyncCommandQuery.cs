using _Main.Scripts.Utils;
using _Main.Scripts.Utils.Commands;

namespace _Main.Scripts.Player.Movement.Commands
{
    public class MovementAsyncCommandQuery : BaseAsyncCommandQuery<MoveInput>
    {
        public new IMoveCommand RunningCommand => (IMoveCommand) base.RunningCommand;

        public MovementAsyncCommandQuery(ICoroutineRunner coroutineRunner) : base(coroutineRunner)
        {
        }
    }
}