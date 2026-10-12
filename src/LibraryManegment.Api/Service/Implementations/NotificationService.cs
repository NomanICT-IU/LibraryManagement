using LibraryManegment.Api.Hubs;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace LibraryManegment.Api.Service.Implementations;

public class NotificationService(IHubContext<NotificationHub> hubContext) : INotificationService
{
    public Task NotifyReviewersAsync(int issueBookId, int roleId, string message)
    {
        return hubContext.Clients
            .Group($"ExtensionRequestReviewers_{roleId}")
            .SendAsync("ReceiveNotification", new
            {
                IssueBookId = issueBookId,
                RoleId = roleId,
                Message = message
            });
    }

    public Task NotifyMemberAsync(int userId, int issueBookId, string message)
    {
        return hubContext.Clients
            .User(userId.ToString())
            .SendAsync("ReceiveNotification", new
            {
                IssueBookId = issueBookId,
                UserId = userId,
                Message = message
            });
    }
}