
namespace LibraryManegment.Api.Service.Interfaces;

public interface INotificationService
{
    Task NotifyReviewersAsync(int issueBookId, int roleId, string message);

    Task NotifyMemberAsync(int userId, int issueBookId, string message);
}
