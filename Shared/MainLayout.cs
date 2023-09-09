using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Kwiz.Dashboard.Shared;

public partial class MainLayout : LayoutComponentBase
{
    // private bool UserInterestsSaved = false;

    // [Inject]
    // public IHttpClientFactory ClientFactory { get; set; }
    // protected override async Task OnInitializedAsync()
    // {
    //     var client = ClientFactory.CreateClient("ServerAPI");
    //     var response = await client.GetAsync("api/v1/userinfo/interests");
    //     if(response.StatusCode is System.Net.HttpStatusCode.NoContent)
    //         UserInterestsSaved = false;

    //     await base.OnInitializedAsync();
    // }
    private IEnumerable<UserInterest> Interests { get; set; }
    
    [Inject]
    public IKwizApiHttpClient KwizApiHttpClient { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Interests = (await KwizApiHttpClient.GetUserInterestsAsync()).ToList();
    }
}

public class UserInterestDto
{
    public string[] Interests { get; set; }  
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}