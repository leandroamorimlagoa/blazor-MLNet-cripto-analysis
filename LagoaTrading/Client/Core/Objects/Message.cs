using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Client.Core.Objects
{
    public class Message
    {
        private Dictionary<MessageType, List<string>> _messages = new Dictionary<MessageType, List<string>>();

        public Message()
        {
            _messages.Add(MessageType.Success, new List<string>());
            _messages.Add(MessageType.Error, new List<string>());
            _messages.Add(MessageType.Warning, new List<string>());
            _messages.Add(MessageType.Info, new List<string>());
        }

        public void AddMessage(MessageType status, string message)
        {
            _messages[status].Add(message);
        }

        public void RemoveMessage(MessageType status)
        {
            _messages[status].Clear();
        }

        public List<string> GetMessages(MessageType status)
        {
            return _messages[status];
        }
    }
}
