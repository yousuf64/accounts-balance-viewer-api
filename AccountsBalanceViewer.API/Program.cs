using AccountsViewer.API.Models.Contexts;
using AccountsViewer.API.Repositories;
using AccountsViewer.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("AccountsDB")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IAuthService, AuthService>();
    services.AddTransient<IAccountService, AccountService>();
    services.AddTransient<IEntryService, EntryService>();
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IAccountRepository, AccountRepository>();
    services.AddTransient<IEntryRepository, EntryRepository>();
}