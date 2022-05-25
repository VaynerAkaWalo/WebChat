using WebChat.Hubs;
using WebChat.Services;
using Microsoft.EntityFrameworkCore;
using WebChat.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
// builder.Services.AddDbContext<MessageContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("MessageContext")));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// builder.Services.AddScoped<IMessageStorage, DatabaseBasedStorage>();
builder.Services.AddSingleton<IMessageStorage, QueueBasedStorage>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
// only for database
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//
//     var context = services.GetRequiredService<MessageContext>();
//     context.Database.EnsureCreated();
// }

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chatHub");

app.Run();

