using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZeroToHeroAPI.Data;
using ZeroToHeroAPI.Exeptions;
using ZeroToHeroAPI.Filters;
using ZeroToHeroAPI.Interface;
using ZeroToHeroAPI.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllers();
builder.Services.AddScoped<ValidateDtoFilter>();
builder.Services.AddScoped<IPlayerStatService, PlayerStatService>();
builder.Services.AddScoped<IQuestService, QuestService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<GlobaleExceptionsHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapIdentityApi<User>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();