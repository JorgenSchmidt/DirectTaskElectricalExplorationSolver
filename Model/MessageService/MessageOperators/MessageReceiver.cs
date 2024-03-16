using Core.Interfaces;
using Model.MessageService.MessageBoxService;

namespace Model.MessageService.MessageOperators
{
    public class MessageReceiver
    {
        private readonly IMessageService _messageBus;

        public MessageReceiver(IMessageService messageBus)
        {
            _messageBus = messageBus;
            _messageBus.Subscribe((string message) =>
            {
                GetMessageBox.Show(message);
            });
        }
    }
}