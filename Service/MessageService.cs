using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Service;

public class MessageService : IMessageService
{
    private readonly DataContext _dbContext;

    public MessageService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddMessageAsync(string content, List<string> tags)
    {
        var message = new Message
        {
            Content = content,
            selectedTags = tags,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Messages.Add(message);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Message>> GetFilteredMessagesAsync(string[]? selectedTags)
    {
        var messages = await _dbContext.Messages.ToListAsync();
        messages = messages.Where(message =>
            message.selectedTags == null || message.selectedTags.Count == 0 ||
            message.selectedTags.Intersect(selectedTags, StringComparer.OrdinalIgnoreCase).Any()
        ).ToList();
        return messages;
    }
}