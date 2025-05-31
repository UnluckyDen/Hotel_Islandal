using System;
using System.Collections;

namespace _Main.Scripts.Utils.Commands
{
    public interface IAsyncCommand
    {
        public CommandStatus Status { get; }
        public int CommandNumber { get; }
        
        public IEnumerator Execute(StopFlag stopFlag, PauseFlag pauseFlag);
        public void UpdateCommandNumber(int commandNumber);
        
        public void Undo();
    }
}