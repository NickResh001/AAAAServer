using ASP_busstation.Models1;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using ASP_busstation.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AspbusstationContext>();
builder.Services.AddDbContext<AspbusstationContext>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.WithOrigins("http://localhost:3000")
     .AllowAnyHeader()
    .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers(); 
builder.Services.AddControllers().AddJsonOptions(x =>
 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "BusstationCookie";
    options.LoginPath = "/";
    options.AccessDeniedPath = "/";
    options.LogoutPath = "/";
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    //  401      
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var bloggingContext = scope.ServiceProvider.GetRequiredService<AspbusstationContext>();
    await BusstationContextSeed.SeedAsync(bloggingContext);
    await IdentitySeed.CreateUserRoles(scope.ServiceProvider);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
