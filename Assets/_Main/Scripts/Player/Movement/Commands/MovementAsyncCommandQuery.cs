using _Main.Scripts.Utils;
using _Main.Scripts.Utils.Commands;

namespace _Main.Scripts.Player.Movement.Commands
{
    public class MovementAsyncCommandQuery : BaseAsyncCommandQuery<MoveInput>
    {
        public MovementAsyncCommandQuery(ICoroutineRunner coroutineRunner) : base(coroutineRunner)
        {
        }
    }
}