using Talkify.Models.Messages;

namespace Talkify.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetMessagesAsync();
        Task<Message> GetMessageAsync(string messageId);
        Task AddMessageAsync(Message message);
    }
}