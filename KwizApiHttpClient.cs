using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Kwiz;

public class KwizApiHttpClient : IKwizApiHttpClient
{
    private readonly HttpClient client;
    private UserInterest[]? userInterest;

    public KwizApiHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<UserInterest[]> GetUserInterestsAsync()
    {
        var httpResponse = await client.GetAsync("api/v1/Userinfo/interests");

        httpResponse.EnsureSuccessStatusCode();

        var content = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UserInterest[]>(
            content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<bool> IsUserInterestsSubmittedAsync()
    {
        try
        {
            var httpResponce = await client.GetAsync("api/v1/Userinfo/interests");

            if(httpResponce.IsSuccessStatusCode)
            {
                var content = await httpResponce.Content.ReadAsStringAsync();
                bool isSubmitted = bool.Parse(content);

                return isSubmitted;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task SubmitUserInterestAsync(UserInterest interests)
    {
        try
        {
            var httpResponce = await client.PostAsJsonAsync("api/v1/Userinfo/interests", interests);
            if(httpResponce.IsSuccessStatusCode)
                return;
            else
                throw new Exception("Not found");
        }
        catch (Exception ex)
        {
            throw new Exception("exception");
        }
    }
}