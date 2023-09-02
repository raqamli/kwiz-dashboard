public interface IKwizApiHttpClient
{
    Task<UserInterest[]> GetUserInterestsAsync();
    Task<bool> IsUserInterestsSubmittedAsync();
    Task SubmitUserInterestAsync(UserInterest interests);
}