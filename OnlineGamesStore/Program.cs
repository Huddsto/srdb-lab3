using Microsoft.EntityFrameworkCore;
using OnlineGamesStore.Implementations.Services;
using OnlineGamesStore.Implementations.Services.Interfaces;
using OnlineGamesStore.Persistence;
using OnlineGamesStore.Persistence.DbContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OnlineGamesStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeveloperService, DeveloperService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OnlineGamesStoreDbContext>();
    db.Database.Migrate();
    db.EnsureStoredProceduresAndFunctions();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
