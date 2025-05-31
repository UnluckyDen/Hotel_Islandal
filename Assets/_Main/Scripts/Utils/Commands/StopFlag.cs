namespace _Main.Scripts.Utils.Commands
{
    public class StopFlag
    {
        public bool IsStop { get; private set; }

        public void Stop()
        {
            IsStop = true;
        }
    }
    
    public class PauseFlag
    {
        public bool IsPause { get; private set; }

        public void Pause()
        {
            IsPause = true;
        }

        public void UnPause()
        {
            IsPause = false;
        }
    }
}