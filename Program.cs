using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? string.Empty;
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();