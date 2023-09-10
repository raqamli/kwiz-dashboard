using Kwiz.Dashboard.Models;

namespace Kwiz.Dashboard.Services;

public interface IKwizApiHttpClient
{
    ValueTask<UserInterests> GetUserInterestsAsync();
    Task<bool> IsUserInterestsSubmittedAsync();
    Task SubmitUserInterestsAsync(IEnumerable<Guid> interestedTechnologyIds);
    ValueTask<UserInterests> GetUserInterestsOrDefaultAsync();
    ValueTask<IEnumerable<Technology>> GetTechnologiesAsync();
}