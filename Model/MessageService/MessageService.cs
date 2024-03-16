using Core.Interfaces;

namespace Model.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly Dictionary<Type, List<object>> _subscribers = new Dictionary<Type, List<object>>();

        public void Publish<T>(T message)
        {
            Type messageType = typeof(T);

            if (_subscribers.ContainsKey(messageType))
            {
                foreach (var subscriber in _subscribers[messageType])
                {
                    ((Action<T>)subscriber)(message);
                }
            }
        }

        public void Subscribe<T>(Action<T> handler)
        {
            Type messageType = typeof(T);

            if (!_subscribers.ContainsKey(messageType))
            {
                _subscribers[messageType] = new List<object>();
            }
            _subscribers[messageType].Add(handler);
        }
    }
}