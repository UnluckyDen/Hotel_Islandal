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
}