public interface IKwizApiHttpClient
{
    ValueTask<IEnumerable<UserInterest>> GetUserInterestsAsync();
    Task<bool> IsUserInterestsSubmittedAsync();
    Task SubmitUserInterestsAsync(UserInterest interests);
    ValueTask<IEnumerable<UserInterest>> GetUserInterestsOrDefaultAsync();
}