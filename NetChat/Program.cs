using Microsoft.EntityFrameworkCore;
using NetChat.Models;
using NetChat.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=NetChat.db")); // Configure EF Core with SQLite

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Path to the login page
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
// Redirect root URL to /Login
app.MapGet("/", async (HttpContext context) =>
{
    if (!context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Login");
    }
    else
    {
        context.Response.Redirect("/Index");
    }
    await Task.CompletedTask;
});

// Redirect logout to the Login page
app.MapPost("/Logout", async (HttpContext context) =>
{
    // Sign out the user
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    // Redirect to the login page after logging out
    context.Response.Redirect("/Login");
});

app.MapHub<ChatHub>("/chatHub");

app.Run();
