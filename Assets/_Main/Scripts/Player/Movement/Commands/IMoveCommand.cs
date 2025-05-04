using _Main.Scripts.Utils.Commands;
using UnityEngine;

namespace _Main.Scripts.Player.Movement.Commands
{
    public interface IMoveCommand : IAsyncCommand
    {
        public Vector2 MoveInput { get; }
    }
}