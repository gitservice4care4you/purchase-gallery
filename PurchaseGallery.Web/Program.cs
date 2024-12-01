using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using PurchaseGallery.Web;
using PurchaseGallery.Web.Components;
using PurchaseGallery.Web.Mappers.Auth;
using PurchaseGallery.Web.Models.Auth;
using static System.Net.Mime.MediaTypeNames;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });


// // Add authentication services
// builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});


// Register Microsoft Identity Web services
builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration)
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddMicrosoftGraph(builder.Configuration.GetSection("Graph"))
    .AddInMemoryTokenCaches();





// Register GraphServiceClient
builder.Services.AddScoped<GraphServiceClient>(sp =>
{
    var tokenAcquisition = sp.GetRequiredService<ITokenAcquisition>();
    var graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(async requestMessage =>
    {
        var token = await tokenAcquisition.GetAccessTokenForUserAsync(new[] { "User.Read" });
        requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }));

    return graphClient;
});



var app = builder.Build();
app.UseSession();
bool authenticated = false;
bool loggedIn = false;
// Middleware to handle login request after Microsoft Identity login
app.Use(async (context, next) =>
{


    try
    {
        // authenticated = false;
        authenticated = context.User.Identity!.IsAuthenticated;
        loggedIn = context.Session.Keys.Contains("UserDetails");
        // loggedIn = false;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);

        loggedIn = false;
    }

    // Console.WriteLine($"Authenticated: {authenticated}, LoggedIn: {loggedIn}");
    if (context.User.Identity!.IsAuthenticated && !loggedIn)
    {
        var tokenAcquisition = context.RequestServices.GetRequiredService<ITokenAcquisition>();
        var token = await tokenAcquisition.GetAccessTokenForUserAsync(["User.Read"], user: context.User);
        var graphClient = context.RequestServices.GetRequiredService<GraphServiceClient>();
        graphClient.AuthenticationProvider = new DelegateAuthenticationProvider(requestMessage =>
        {
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return Task.CompletedTask;
        });
        var user = await graphClient.Me.Request().GetAsync();
        UserDetails userDetails = new()
        {
            MicrosoftAccountId = user.Id,
            FullName = user.DisplayName,
            EmailAddress = user.Mail ?? user.UserPrincipalName,
            Department = user.Department,
            Country = user.Country,
            JobTitle = user.JobTitle
        };
        context.Session.SetString("UserDetails", System.Text.Json.JsonSerializer.Serialize(userDetails));

        HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5059")

        };
        Console.WriteLine($"Usersss: {UserDetailsMapper.ConvertUserDetailsToJson(userDetails)}");
        var content = new StringContent(UserDetailsMapper.ConvertUserDetailsToJson(userDetails), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Response: {apiResponse}");
        }
        else
        {
            Console.WriteLine($"Failed to login to backend API. Status Code: {response.StatusCode}");
        }

        Console.WriteLine($"User details: {userDetails}");
        // You can now use userDetails as needed
    }

    await next();
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGet("/authentication/logout", async context =>
{
    await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

app.MapDefaultEndpoints();

app.Run();
