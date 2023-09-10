using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class KwizApiHttpClient : IKwizApiHttpClient
{
    private readonly HttpClient client;

    public KwizApiHttpClient(HttpClient client)
    {
        this.client = client;
    }

    // private readonly HttpClient client;

    // public KwizApiHttpClient(IHttpClientFactory httpClientFactory)
    // {
    //     this.client = httpClientFactory.CreateClient("KwizApiBaseUrl");
    // }

    public async ValueTask<IEnumerable<Technologies>> GetTechnologiesAsync()
    {
        var httpResponse = await client.GetAsync("api/v1/Userinfo/technologies");

        httpResponse.EnsureSuccessStatusCode();

        var content = await httpResponse.Content.ReadAsStringAsync();
        var technologies = JsonConvert.DeserializeObject<IEnumerable<Technologies>>(content);

        return technologies;
    }

    public async ValueTask<IEnumerable<UserInterest>> GetUserInterestsAsync()
    {
        var httpResponse = await client.GetAsync("api/v1/Userinfo/interests");

        httpResponse.EnsureSuccessStatusCode();

        var content = await httpResponse.Content.ReadAsStringAsync();
        var userInterests = JsonConvert.DeserializeObject<IEnumerable<UserInterest>>(content);

        return userInterests;
    }

    public async ValueTask<IEnumerable<UserInterest>> GetUserInterestsOrDefaultAsync()
    {
        try
        {
            var userInterests = await GetUserInterestsAsync();
            return userInterests;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred while getting user interests: {ex.Message}");
            return Enumerable.Empty<UserInterest>();
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

    public async Task SubmitUserInterestsAsync(UserInterest interests)
    {
        try
        {
            var serializedInterests = JsonConvert.SerializeObject(interests);
            var content = new StringContent(serializedInterests, Encoding.UTF8, "application/json");  

            var response = await client.PostAsync("api/user/interests", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to submit user interests.", ex);
        }
    }

    
}