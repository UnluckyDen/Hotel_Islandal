using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _Main.Scripts.Utils.Commands
{
    public class BaseAsyncCommandQuery<T>
    {
        public event Action<IAsyncCommand> CommandSucceeded;
        public event Action<IAsyncCommand> CommandFailure;
        public event Action<IAsyncCommand> CommandInterrupted;
        public event Action<IAsyncCommand> CommandRemoved;
        public event Action CommandsDiscarded;
        
        private readonly List<IAsyncCommand> _commands = new();
        private readonly ICoroutineRunner _coroutineRunner;
        private StopFlag _stopFlag;
        private PauseFlag _pauseFlag;
        
        public IAsyncCommand RunningCommand {get; private set; }
        public IReadOnlyCollection<IAsyncCommand> Commands => _commands;
        public bool IsRunning => RunningCommand != null;

        public BaseAsyncCommandQuery(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _pauseFlag = new PauseFlag();
        }

        public void StartQueue()
        {
            if (IsRunning)
                return;
            
            _coroutineRunner.StartCoroutine(Loop());
        }

        public void PauseQueue()
        {
            if (IsRunning && _pauseFlag != null)
                _pauseFlag.Pause();
        }

        public void UnPauseQueue()
        {
            if (IsRunning && _pauseFlag != null)
                _pauseFlag.UnPause();
        }
        
        public void Append(IAsyncCommand command)
        {
            _commands.Add(command);
            UpdateCommandNumbers();
        }
        
        public void Prepend(IAsyncCommand command)
        {
            _commands.Insert(0, command);
            UpdateCommandNumbers();
        }
        
        public void Remove(IAsyncCommand command)
        {
            _commands.Remove(command);
            UpdateCommandNumbers();
            CommandRemoved?.Invoke(command);
        }

        public void DiscardAllCommands()
        {
            _commands.Clear();
            
            if (_stopFlag != null)
                _stopFlag.Stop();
            
            CommandsDiscarded?.Invoke();
        }

        private IEnumerator Loop()
        {
            _stopFlag = new StopFlag();
            
            while (_commands.Count > 0)
            {
                if (_stopFlag.IsStop)
                    break;
                
                RunningCommand = _commands.First();
                _commands.RemoveAt(0);
                
                yield return RunningCommand.Execute(_stopFlag, _pauseFlag);
                var result = RunningCommand.Status;
                
                UpdateCommandNumbers();
                
                CallEvent(RunningCommand);
                RunningCommand = null;
               
                if (result == CommandStatus.Failure)
                    break;
            }

            _stopFlag = null;
            yield break;
        }
        
        private void CallEvent(IAsyncCommand command)
        {
            switch (command.Status)
            {
                case CommandStatus.Success:
                    CommandSucceeded?.Invoke(command);
                    break;
                case CommandStatus.Failure:
                    CommandFailure?.Invoke(command);
                    break;
                case CommandStatus.Interrupted:
                    CommandInterrupted?.Invoke(command);
                    break;
                case CommandStatus.Running:
                case CommandStatus.NotStarted:
                default:
                    throw new NotImplementedException($"Not expected of type {command.GetType()} - {command} to be in {command.Status}");
            }
        }

        private void UpdateCommandNumbers()
        {
            foreach (var command in _commands)
                command.UpdateCommandNumber(_commands.IndexOf(command) + 1);
        }
    }
}