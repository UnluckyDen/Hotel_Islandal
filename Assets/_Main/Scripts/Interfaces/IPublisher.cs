using System;

namespace _Main.Scripts.Interfaces
{
    public interface IPublisher<T>
    {
        public void Subscribe(Action<T> action);
        public void Unsubscribe(Action<T> action);
    }
}