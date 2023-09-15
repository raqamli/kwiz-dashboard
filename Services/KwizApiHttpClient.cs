using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Kwiz.Dashboard.Models;

namespace Kwiz.Dashboard.Services;

public class KwizApiHttpClient : IKwizApiHttpClient
{
    private readonly HttpClient client;
    private readonly DataService dataService;

    public KwizApiHttpClient(
        HttpClient client,
        DataService dataService)
    {
        this.client = client;
        this.dataService = dataService;
    }

    public async ValueTask<IEnumerable<Technology>> GetTechnologiesAsync()
    {
        var httpResponse = await client.GetAsync("api/technologies");

        httpResponse.EnsureSuccessStatusCode();

        var content = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<Technology>>(content);
    }

    public async ValueTask<UserInterests> GetUserInterestsAsync()
    {
        var httpResponse = await client.GetAsync("api/v1/Userinfo/interests");

        httpResponse.EnsureSuccessStatusCode();

        var content = await httpResponse.Content.ReadAsStringAsync();
        if(httpResponse.StatusCode == System.Net.HttpStatusCode.NoContent || string.IsNullOrWhiteSpace(content))
            return default;
        
        return JsonSerializer.Deserialize<UserInterests>(content);
    }

    public async ValueTask<UserInterests> GetUserInterestsOrDefaultAsync()
    {
        try
        {
            var userInterests = await GetUserInterestsAsync();
            return userInterests;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred while getting user interests: {ex.Message}");
            return default;
        }
    }
    public async Task<bool> IsUserInterestsSubmittedAsync()
    {
        try
        {
            var interests = await GetUserInterestsOrDefaultAsync();
            return interests != null;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to check if user interests are submitted.", ex);
        }
    }

    public async Task SubmitUserInterestsAsync(IEnumerable<Guid> interestedTechnologyIds)
    {
        var response = await client.PostAsJsonAsync("api/v1/Userinfo/interests", interestedTechnologyIds);
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateQuizAsync(CreateQuiz quiz)
    {
        var response = await client.PostAsJsonAsync("api/v1/Quizes", quiz);
        await response.Content.ReadFromJsonAsync<CreateQuiz>();
        response.EnsureSuccessStatusCode();
    }

    public async ValueTask<GetQuiz> GetQuizzesAsync(int pageIndex = 0, int pageSize = 5)
    {
        var response = await client.GetAsync($"api/v1/Quizes?offset={pageIndex}&limit={pageSize}");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GetQuiz>(content);
    }

    public async ValueTask<GetQuestion> GetQuestionsAsync(Guid quizId, int pageIndex, int pageSize)
    {
        var response = await client.GetAsync($"api/v1/Quizes/{quizId}/questions?offset={pageIndex}&limit={pageSize}");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GetQuestion>(content);
    }

    public async ValueTask<GetQuiz> GetQuizTitleAsync(string title, int pageIndex = 0, int pageSize = 1)
    {
        var response = await client.GetAsync($"api/v1/Quizes?search={title}&offset={pageIndex}&limit={pageSize}");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GetQuiz>(content);
    }

    public async Task CreateQuestionAsync(Guid quizId, CreateQuestion question)
    {
        var response = await client.PostAsJsonAsync($"api/v1/Quizes/{quizId}/question", question);
        await response.Content.ReadFromJsonAsync<CreateQuestion>();
        
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateOptionsAsync(Guid questionId, IEnumerable<CreateOptions> options)
    {
        var response = await client.PostAsJsonAsync($"api/v1/Questions/{questionId}/questionOptions", options);
        await response.Content.ReadFromJsonAsync<CreateOptions>();
        response.EnsureSuccessStatusCode();
    }
}