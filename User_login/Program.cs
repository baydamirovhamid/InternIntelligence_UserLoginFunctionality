using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using User_login.Contexts;
using User_login.Models;
using User_login.Repositories;
using User_login.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false; 
    options.Password.RequireLowercase = false; 
    options.Password.RequireUppercase = false; 
    options.Password.RequireNonAlphanumeric = false;

});

builder.Services.AddScoped<AuthenticationContext>();
builder.Services.AddScoped<IAuthenticationStrategyService, UsernameAuthenticationStrategyService>();
builder.Services.AddScoped<IAuthenticationStrategyService, EmailAuthenticationStrategyService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();





var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapControllers();

app.Run();
