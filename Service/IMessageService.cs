using ChatApp.Models;

namespace ChatApp.Service;

public interface IMessageService
{
    Task AddMessageAsync(string content, List<string> tags);
    Task<List<Message>> GetFilteredMessagesAsync(string[] selectedTags);
}