using Core.Interfaces;
using Model.MessageService.MessageOperators;

namespace Model.MessageService.MessageEntities
{
    public class MessageObjects
    {
        private static IMessageService _messageService = new MessageService();

        public static MessageSender Sender = new MessageSender(_messageService);
        public static MessageReceiver Receiver = new MessageReceiver(_messageService);
    }
}