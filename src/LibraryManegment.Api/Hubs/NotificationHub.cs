using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LibraryManegment.Api.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var canReviewRequests = Context.User?.Claims.Any(
            claim => claim.Type == "permission" &&
                     claim.Value == "ExtensionRequest.Read") == true;

        var roleIdValue = Context.User?.FindFirst("roleId")?.Value;

        if (canReviewRequests &&
            int.TryParse(roleIdValue, out var roleId))
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"ExtensionRequestReviewers_{roleId}");
        }

        await base.OnConnectedAsync();
    }
}