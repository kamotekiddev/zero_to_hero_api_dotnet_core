using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Exeptions;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;
using ZeroToHeroAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers(options => options.Filters.Add<ExceptionFilter>());
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services.AddScoped<ValidateDtoFilter>();
builder.Services.AddScoped<ExceptionFilter>();
builder.Services.AddScoped<IPlayerStatService, PlayerStatService>();
builder.Services.AddScoped<IQuestService, QuestService>();
builder.Services.AddScoped<IQuestActionService, QuestActionService>();
builder.Services.AddScoped<IQuestRewardService, QuestRewardService>();
builder.Services.AddScoped<IQuestPunishmentService, QuestPunishmentService>();
builder.Services.AddScoped<IDailyQuestService, DailyQuestService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await AdminSeeder.SeedAdminAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapIdentityApi<User>();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();