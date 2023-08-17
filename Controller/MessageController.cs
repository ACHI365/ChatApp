using ChatApp.Chat;
using ChatApp.Service;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Models.Dto;

namespace ChatApp.Controller;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IHubContext<ChatHub> _chatHub;

    public MessageController(IMessageService messageService, IHubContext<ChatHub> chatHub)
    {
        _messageService = messageService;
        _chatHub = chatHub;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] MessageDto? model)
    {
        if (model == null)
        {
            return BadRequest("Invalid request data.");
        }

        await _messageService.AddMessageAsync(model.Message, model.Tags);
        await _chatHub.Clients.All.SendAsync("ReceiveMessage", model.Message, model.Tags);

        return Ok();
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetMessages([FromQuery] string? selectedTags)
    {
        var decodedTags = string.IsNullOrEmpty(selectedTags) ? new string[] { selectedTags } :  Uri.UnescapeDataString(selectedTags).Split(',').ToArray();
        var messages = await _messageService.GetFilteredMessagesAsync(decodedTags);
        return Ok(messages);
    }
}