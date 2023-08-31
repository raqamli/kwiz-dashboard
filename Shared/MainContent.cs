using Microsoft.AspNetCore.Components;

namespace Kwiz.Dashboard.Shared;

public partial class MainContent : LayoutComponentBase
{
    private bool userInterestsSaved = true;
    [Inject]
    public IHttpClientFactory ClientFactory { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var client = ClientFactory.CreateClient("KwizAPI");
        var response = await client.GetAsync("api/v1/userinfo/interests");
        if(response.StatusCode is System.Net.HttpStatusCode.NoContent)
            userInterestsSaved = false;
        
        await base.OnInitializedAsync();
    }
}

public class UserInterestDto
{
    public string[] Interests { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}