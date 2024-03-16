namespace Core.Interfaces
{
    public interface IMessageService
    {
        void Publish<T>(T message);
        void Subscribe<T>(Action<T> handler);
    }
}