using JetBrains.Annotations;

namespace _Main.Scripts.Utils.Commands
{
    public interface ICommand<T>
    {
        public void Execute([CanBeNull] T context);

        public void Undo();
    }
}