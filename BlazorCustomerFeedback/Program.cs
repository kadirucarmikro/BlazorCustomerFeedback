using BlazorCustomerFeedback.Client.Pages;
using BlazorCustomerFeedback.Components;
using BlazorCustomerFeedback.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add Redis configuration
builder.Services
    .AddRedisCache(builder.Configuration["Redis:ConnectionString"]);

builder.Services
    .AddDbContext<BlazorCustomerFeedback.Repositories.FeedbackDbContext>(
    options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorCustomerFeedback.Client._Imports).Assembly);

app.Run();
