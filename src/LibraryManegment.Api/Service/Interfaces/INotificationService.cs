
namespace LibraryManegment.Api.Service.Interfaces;

public interface INotificationService
{
    Task NotifyReviewersAsync(int requestId, string message);

    Task NotifyMemberAsync(int memberUserId, int requestId, string message);
}
