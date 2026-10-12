
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LibraryManegment.Api.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var canReviewRequests = Context.User?.Claims.Any(claim =>
            claim.Type == "permission" &&
            claim.Value == "ExtensionRequest.Read") == true;

        if (canReviewRequests)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                "ExtensionRequestReviewers");
        }

        await base.OnConnectedAsync();
    }
}
