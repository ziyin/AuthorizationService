using CustomerAuthorization;
using CustomerAuthorization.Implements;
using CustomerAuthorization.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WebService.Authorization.Application;
using WebService.Authorization.HttpApi.Host.DependencyInjection;
using WebService.Authorization.HttpApi.Host.Transformer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var confiruare = builder.Configuration;
builder.Services.DependencyInjectionOption(confiruare);
builder.Services.DependencyInjectionClass();

#region Auth

builder.Services.AddJwtSdk(confiruare);
var jwtSection = builder.Configuration.GetSection("JwtToken");
var issuer = jwtSection["Issuer"];
var audience = jwtSection["Audience"];
var key = jwtSection["Key"];

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IGetCurrentUser>(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var user = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
    return new GetCurrentUser(user);
});
#endregion

builder.Services.AddMapster();
ApplicationMappingConfig.RegisterMappings();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
