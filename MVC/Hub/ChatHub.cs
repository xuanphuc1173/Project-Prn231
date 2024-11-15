﻿using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace MVC.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly ConcurrentDictionary<string, string> ConnectedUsers = new();

        public ChatHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override async Task OnConnectedAsync()
        {
            var accountId = _httpContextAccessor.HttpContext.Session.GetString("CustomerId");
            if (accountId != null)
            {
                ConnectedUsers[accountId] = Context.ConnectionId; 
                await Clients.All.SendAsync("UserConnected", accountId);
            }
            await base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var accountId = _httpContextAccessor.HttpContext.Session.GetString("AccountId");
            if (accountId != null)
            {
                ConnectedUsers.TryRemove(accountId, out _); 
                await Clients.All.SendAsync("UserDisconnected", accountId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
