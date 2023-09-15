using Kwiz.Dashboard.Models;

namespace Kwiz.Dashboard.Services;

public interface IKwizApiHttpClient
{
    ValueTask<UserInterests> GetUserInterestsAsync();
    Task<bool> IsUserInterestsSubmittedAsync();
    Task SubmitUserInterestsAsync(IEnumerable<Guid> interestedTechnologyIds);
    ValueTask<UserInterests> GetUserInterestsOrDefaultAsync();
    ValueTask<IEnumerable<Technology>> GetTechnologiesAsync();
    Task CreateQuizAsync(CreateQuiz quiz);
    ValueTask<GetQuiz> GetQuizzesAsync(int pageIndex, int pageSize);
    ValueTask<GetQuiz> GetQuizTitleAsync(string title, int pageIndex, int pageSize);
    ValueTask<GetQuestion> GetQuestionsAsync(Guid quizId, int pageIndex, int pageSize);
    Task CreateQuestionAsync(Guid quizId, CreateQuestion question);
    Task CreateOptionsAsync(Guid questionId, IEnumerable<CreateOptions> options);
    ValueTask<string> GetQuestionsLastIdAsync();
}