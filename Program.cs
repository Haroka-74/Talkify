using Talkify.Data;
using Microsoft.EntityFrameworkCore;
using Talkify.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Talkify.Repositories.Interfaces;
using Talkify.Repositories.Implementations;
using Talkify.Services.Interfaces;
using Talkify.Services.Implementations;
using Talkify.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TalkifyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<TalkifyUser, IdentityRole>()
    .AddEntityFrameworkStores<TalkifyDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
});
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IChatService, ChatService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chatHub");
app.Run();