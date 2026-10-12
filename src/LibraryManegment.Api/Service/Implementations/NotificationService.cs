
using LibraryManegment.Api.Hubs;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace LibraryManegment.Api.Service.Implementations;

public class NotificationService(IHubContext<NotificationHub> hubContext) : INotificationService
{
    public Task NotifyReviewersAsync(int requestId, string message)
    {
        return hubContext.Clients
            .Group("ExtensionRequestReviewers")
            .SendAsync("ReceiveNotification", new
            {
                RequestId = requestId,
                Message = message
            });
    }

    public Task NotifyMemberAsync(int memberUserId, int requestId, string message)
    {
        return hubContext.Clients
            .User(memberUserId.ToString())
            .SendAsync("ReceiveNotification", new
            {
                RequestId = requestId,
                Message = message
            });
    }
}
