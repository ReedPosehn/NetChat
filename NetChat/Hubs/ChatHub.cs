using Microsoft.AspNetCore.SignalR;
using NetChat.Models;
using System;
using System.Threading.Tasks;

namespace NetChat.Hubs;

public class ChatHub : Hub
{
    private readonly AppDbContext _context;

    public ChatHub(AppDbContext context)
    {
        _context = context;
    }

    // Method called by clients to send a message
    public async Task SendMessage(string user, string messageText)
    {
        // Create a new message instance
        var message = new Message
        {
            User = user,
            Content = messageText,
            Timestamp = DateTime.Now
        };

        // Save the message to the database
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        // Broadcast the message to all connected clients
        await Clients.All.SendAsync("ReceiveMessage", user, messageText, message.Timestamp);
    }

    // Optional: You can add other methods as needed, e.g., for connection management
}
