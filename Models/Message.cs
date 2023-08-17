using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ChatApp.Models;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string TagsJson { get; set; }

    [NotMapped]
    public List<string> selectedTags
    {
        get => TagsJson != null ? JsonSerializer.Deserialize<List<string>>(TagsJson) : new List<string>();
        set => TagsJson = JsonSerializer.Serialize(value);
    }
    
    public DateTime CreatedAt { get; set; }
}
