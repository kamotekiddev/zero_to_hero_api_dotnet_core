using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using ZeroToHeroAPI.BackgroundJobs;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Exeptions;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Models;
using ZeroToHeroAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey(nameof(AutoAssignQuestJob));
    var triggerKey = new TriggerKey($"{nameof(AutoAssignQuestJob)}Trigger");

    q.AddJob<AutoAssignQuestJob>(options => options.WithIdentity(jobKey));
    q.AddTrigger(options => options.ForJob(jobKey)
        .WithIdentity(triggerKey)
        .WithCronSchedule("0 0 1 * * ?"));


    var failJobKey = new JobKey(nameof(AutoFailQuestJob));
    var failTriggerKey = new TriggerKey($"{nameof(AutoFailQuestJob)}Trigger");

    q.AddJob<AutoFailQuestJob>(opts => opts.WithIdentity(failJobKey));
    q.AddTrigger(opts => opts
        .ForJob(failJobKey)
        .WithIdentity(failTriggerKey)
        .WithCronSchedule("0 0 0 * * ?"));
});

builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ValidateDtoFilter>();
builder.Services.AddScoped<ExceptionFilter>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IQuestTemplateService, QuestTemplateService>();
builder.Services.AddScoped<IQuestActionService, QuestActionService>();
builder.Services.AddScoped<IQuestRewardService, QuestRewardService>();
builder.Services.AddScoped<IQuestPunishmentService, QuestPunishmentService>();
builder.Services.AddScoped<IDailyQuestService, DailyQuestService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<TokenService>();


var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     await AdminSeeder.SeedAdminAsync(services);
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();